using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class AdminController : Controller
{
    public string Index()
    {
        return $"Testando o método Index de TestController: {DateTime.Now}";
    }

    public string Demo()
    {
        return $"Testando o método Demo de TestController: {DateTime.Now}";
    }
}