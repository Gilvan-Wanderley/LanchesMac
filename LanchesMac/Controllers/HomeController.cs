using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

namespace LanchesMac.Controllers;

public class HomeController : Controller
{   
    private readonly ILancheRepository lancheRepository;

    public HomeController(ILancheRepository lancheRepository)
    {
        this.lancheRepository = lancheRepository;
    }

    public IActionResult Index()
    {
        var homeViewmodel = new HomeViewModel()
        {
            LanchesPreferidos = lancheRepository.LanchesPreferidos
        };
        return View(homeViewmodel);
    }

   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
