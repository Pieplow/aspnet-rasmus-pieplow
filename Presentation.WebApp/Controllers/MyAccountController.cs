using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Memberships;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class MyAccountController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IMembershipService _membershipService;

    public MyAccountController(
        IBookingService bookingService,
        IMembershipService membershipService)
    {
        _bookingService = bookingService;
        _membershipService = membershipService;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Hämta UserId som string (vilket NameIdentifier alltid är)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

      
        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        var membership = await _membershipService.GetByUserIdAsync(userId, HttpContext.RequestAborted);

        var vm = new MyAccountViewModel
        {
            Bookings = bookings,
            Membership = membership,
            Email = email ?? "No email found",
        };

        return View(vm);
    }
}