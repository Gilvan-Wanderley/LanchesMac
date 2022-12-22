using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel{ReturnUrl = returnUrl});
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if(!ModelState.IsValid)
        {
            return View(loginVM);
        }        
        var user = await userManager.FindByNameAsync(loginVM.UserName);
        if(user is not null)
        {
            var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }
        }
        ModelState.AddModelError("", "Falha ao realizar o login!!");
        return View(loginVM);           
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel registroVM)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser(){UserName = registroVM.UserName};
            var result = await userManager.CreateAsync(user, registroVM.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                this.ModelState.AddModelError("Registro","Falha ao realizar o registro");
            }
        }
        return View(registroVM);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
}