using Application.Account;
using Application.Memberships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class MyAccountController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IMembershipService _membershipService;
    private readonly IIdentityService _identityService; 

    public MyAccountController(
        IBookingService bookingService,
        IMembershipService membershipService,
        IIdentityService identityService) 
    {
        _bookingService = bookingService;
        _membershipService = membershipService;
        _identityService = identityService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        // VIKTIG: Denna rad hämtar förnamn, efternamn och tel från databasen
        var user = await _identityService.GetMyAccountAsync(userId);

        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        var membership = await _membershipService.GetByUserIdAsync(userId, HttpContext.RequestAborted);

        var vm = new MyAccountViewModel
        {
           
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email ?? "No email found",
            Bookings = bookings,
            Membership = membership
        };

        return View(vm);
    }

    
    public IActionResult NotFoundPage() => NotFound();
}