using Application.Account;
using Application.Account.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity; // Krävs för SignInResult

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
            // Enkel notis istället för ModelState
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

    // ---------------- EXTERNAL LOGIN (Fixar 404-felet) ----------------

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        // Skapar callback-länken
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });

        // Hämtar konfiguration för Google/GitHub från din service
        var properties = identityService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        // Skickar användaren till Google/GitHub
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

        // 1. Försök logga in om användaren redan finns
        var result = await identityService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Schedule", "Booking");
        }

       
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (!string.IsNullOrEmpty(email))
        {
           
            var command = new RegisterUserCommand(email, "");
            var registrationResult = await identityService.RegisterUserAsync(command);

            if (registrationResult.Succeeded)
            {
                
                await identityService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
                return RedirectToAction("Schedule", "Booking");
            }

            
            TempData["StatusMessage"] = registrationResult.Errors.FirstOrDefault() ?? "Could not register user.";
        }
        else
        {
            TempData["StatusMessage"] = "Could not retrieve email from the external provider.";
        }

        return RedirectToAction("Login");
    }

    // ---------------- ÖVRIGT ----------------

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
        if (userId == null) return Unauthorized();

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
        TempData["Success"] = "Your profile has been successfully updated!";
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