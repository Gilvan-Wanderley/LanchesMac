using LanchesMac.Repositories.Interfaces;
using LanchesMac.Models;
using LanchesMac.Context;

namespace LanchesMac.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext context;

    public CategoriaRepository(AppDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Categoria> Categorias => context.Categorias;
}