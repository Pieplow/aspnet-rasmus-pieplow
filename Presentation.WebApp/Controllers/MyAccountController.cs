using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class MyAccountController : Controller
{
    public IActionResult Index()
    {
        return View(new MyAccountViewModel());
    }
}