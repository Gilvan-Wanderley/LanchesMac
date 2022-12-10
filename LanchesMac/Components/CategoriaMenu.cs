using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CategoriaMenu : ViewComponent
{
    private ICategoriaRepository categoriaRepository;

    public CategoriaMenu(ICategoriaRepository categoriaRepository)
    {
        this.categoriaRepository = categoriaRepository;
    }

    public IViewComponentResult Invoke()
    { 
        var categorias = categoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
        return View(categorias);
    }
}