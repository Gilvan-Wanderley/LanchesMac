using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class ContatoController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();        
    }
}