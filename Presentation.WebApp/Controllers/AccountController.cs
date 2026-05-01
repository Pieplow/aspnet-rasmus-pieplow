using Application.Account;
using Application.Account.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;



namespace Presentation.WebApp.Controllers;

public class AccountController(IIdentityService identityService) : Controller
{
    // ---------------- GET ----------------
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
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
        var email = TempData["Email"] as string;

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Register");

        ViewBag.Email = email;
        TempData.Keep("Email");

        return View();
    }

    // ---------------- POST ----------------
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View();

        var success = await identityService.LoginAsync(email, password);

        if (!success)
        {
            ModelState.AddModelError("", "Invalid login");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Schedule", "Booking");
    }

    // STEP 1: bara email
    [HttpPost]
    public IActionResult Register(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError("", "Email is required");
            return View();
        }

        TempData["Email"] = email;
        return RedirectToAction("SetPassword");
    }

    // STEP 2: skapa user
    [HttpPost]
    public async Task<IActionResult> SetPassword(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Email and password are required");
            ViewBag.Email = email;
            return View();
        }

        var command = new RegisterUserCommand(email, password);
        var result = await identityService.RegisterUserAsync(command);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            ViewBag.Email = email;
            return View();
        }

        return RedirectToAction("Login");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {   
        await identityService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyAccount()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) 
        {
            return Unauthorized();
        } 
        var user = await identityService.GetMyAccountAsync(userId);

        var vm = new MyAccountViewModel
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(string firstName, string lastName, string phoneNumber)
    {
        await identityService.UpdateProfileAsync(User, firstName, lastName, phoneNumber);
        return RedirectToAction("Index", "MyAccount");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        await identityService.DeleteCurrentUserAsync(User);
        await identityService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}