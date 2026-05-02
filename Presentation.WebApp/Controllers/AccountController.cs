using Application.Account;
using Application.Account.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            TempData["StatusMessage"] = "Email and password are required.";
            return View();
        }

        var success = await identityService.LoginAsync(email, password);
        if (!success)
        {
            TempData["StatusMessage"] = "Invalid email or password.";
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Schedule", "Booking");
    }

    [HttpPost]
    public IActionResult Register(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["StatusMessage"] = "Email is required to start registration.";
            return View();
        }
        TempData["Email"] = email;
        return RedirectToAction("SetPassword");
    }

    [HttpPost]
    public async Task<IActionResult> SetPassword(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            TempData["StatusMessage"] = "Both email and password must be provided.";
            ViewBag.Email = email;
            return View();
        }

        var command = new RegisterUserCommand(email, password);
        var result = await identityService.RegisterUserAsync(command);
        if (!result.Succeeded)
        {
            TempData["StatusMessage"] = result.Errors.FirstOrDefault() ?? "Registration failed.";
            ViewBag.Email = email;
            return View();
        }

        TempData["StatusMessage"] = "Account created! You can now log in.";
        return RedirectToAction("Login");
    }

    // ---------------- EXTERNAL LOGIN (Google/GitHub) ----------------

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
        var properties = identityService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        if (remoteError != null)
        {
            TempData["StatusMessage"] = $"Error from external provider: {remoteError}";
            return RedirectToAction("Login");
        }

        var info = await identityService.GetExternalLoginInfoAsync();
        if (info == null)
        {
            TempData["StatusMessage"] = "Error loading external login information.";
            return RedirectToAction("Login");
        }

        var result = await identityService.HandleExternalLoginAsync(info);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Schedule", "Booking");
        }

        TempData["StatusMessage"] = "Failed to log in with external provider.";
        return RedirectToAction("Login");
    }

    // ---------------- LOGOUT ----------------

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await identityService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}