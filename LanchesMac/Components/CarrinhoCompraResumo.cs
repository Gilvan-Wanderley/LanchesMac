using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra carrinhoCompra;

    public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
    {
        this.carrinhoCompra = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
    {
        carrinhoCompra.CarrinhoCompraItens = carrinhoCompra.GetCarrinhoCompraItens();

        var carrinhoCompraVM = new CarrinhoCompraViewModel{
            CarrinhoCompra = carrinhoCompra,
            CarrinhoCOmpraTotal = carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }
}