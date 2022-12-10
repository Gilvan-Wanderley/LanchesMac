using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using LanchesMac.Models;

namespace LanchesMac.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        this.lancheRepository = lancheRepository;
    }

    public IActionResult List(string categoria="")
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual  = string.Empty;

        if(string.IsNullOrEmpty(categoria))
        {
            lanches = lancheRepository.Lanches.OrderBy(l => l.LancheId);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                                              .OrderBy(l => l.Nome);
            categoriaAtual = categoria;
        }

        var lancheListViewModel = new LancheListViewModel{
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(lancheListViewModel);
    }

    public IActionResult Details(int lancheId)
    {
        var lanche = lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        return View(lanche);
    }
}