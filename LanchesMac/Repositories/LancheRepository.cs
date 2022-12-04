using LanchesMac.Repositories.Interfaces;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext context;

    public LancheRepository(AppDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Lanche> Lanches => context.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => context.Lanches.Where(l => l.IsLanchePreferido)
                                                                   .Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
    }
}