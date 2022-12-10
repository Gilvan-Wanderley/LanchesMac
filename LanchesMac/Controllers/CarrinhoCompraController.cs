using LanchesMac.Models;
using LanchesMac.ViewModels;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository lancheRepository;
    private readonly CarrinhoCompra carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
    {
        this.lancheRepository = lancheRepository;
        this.carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        carrinhoCompra.CarrinhoCompraItens = carrinhoCompra.GetCarrinhoCompraItens();

        var carrinhoCompraVM = new CarrinhoCompraViewModel{
            CarrinhoCompra = carrinhoCompra,
            CarrinhoCOmpraTotal = carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }

    public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        
        if (lancheSelecionado is not null)
        {
            carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
        }
        return RedirectToAction("Index");
    }

    public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        
        if (lancheSelecionado is not null)
        {
            carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
        }
        return RedirectToAction("Index");
    }
}