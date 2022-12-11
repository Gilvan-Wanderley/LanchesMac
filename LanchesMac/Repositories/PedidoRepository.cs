using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext appDbContext;
    private readonly CarrinhoCompra carrinhoCompra;

    public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
    {
        this.appDbContext = appDbContext;
        this.carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(Pedido pedido)
    {
        pedido.PedidoEnviado = DateTime.Now;
        appDbContext.Pedidos.Add(pedido);
        appDbContext.SaveChanges();

        var carrinhoCompraItens = carrinhoCompra.CarrinhoCompraItens;

        foreach (var carrinhoCompraItem in carrinhoCompraItens)
        {
            var pedidoDetalhe = new PedidoDetalhe()
            {
                Quantidade = carrinhoCompraItem.Quantidade,
                LancheId = carrinhoCompraItem.Lanche.LancheId,
                PedidoId = pedido.PedidoId,
                Preco = carrinhoCompraItem.Lanche.Preco
            };
            appDbContext.PedidoDetalhes.Add(pedidoDetalhe);
        }
        appDbContext.SaveChanges();
    }
}
