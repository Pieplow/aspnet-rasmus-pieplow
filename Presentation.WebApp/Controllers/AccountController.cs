using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SetPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        return RedirectToAction("Schedule", "Booking");
    }

    [HttpPost]
    public IActionResult Register(string email)
    {
        return RedirectToAction("Login");
    }
}