using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class TesteController : Controller
{
    public string Index()
    {
        return $"Testando o método Index de TestController: {DateTime.Now}";
    }
}