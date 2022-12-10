using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
 

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext context;

    public CarrinhoCompra(AppDbContext context)
    {
        this.context = context;
    }

    public string CarrinhoCompraId {get; set;}
    public List<CarrinhoCompraItem> CarrinhoCompraItens {get; set;}

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<AppDbContext>();
        var carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        session.SetString("CarrinhoId", carrinhoId);

        return new CarrinhoCompra(context){CarrinhoCompraId = carrinhoId};
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = GetCarrinhoCompraItem(lanche);

        if (carrinhoCompraItem is null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem{
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1,
            };
            context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;            
        }
        context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = GetCarrinhoCompraItem(lanche);
        var quantidadeLocal = 0;
        if(carrinhoCompraItem is not null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade--;
            }
            else
            {
                context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
            context.SaveChanges();
        }
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItens ?? 
               ( CarrinhoCompraItens = context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                                                  .Include(s => s.Lanche)
                                                                  .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId);
        context.RemoveRange(carrinhoItens);
        context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        return context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                          .Sum(i => i.Quantidade*i.Lanche.Preco);
    }

    private CarrinhoCompraItem GetCarrinhoCompraItem(Lanche lanche)
    {
        return context.CarrinhoCompraItens.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId && 
                 s.CarrinhoCompraId == CarrinhoCompraId);
    }

}
