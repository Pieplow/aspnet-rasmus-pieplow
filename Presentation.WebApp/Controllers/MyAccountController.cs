using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Bookings;
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
      

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        if (!int.TryParse(userId, out var userIdInt))
        {
            return RedirectToAction("Login", "Account");
        }

        var bookings = await _bookingService.GetUserBookingsAsync(userId); 
        var membership = await _membershipService.GetByUserIdAsync(userIdInt); 

        

        var vm = new MyAccountViewModel
        {
            Bookings = bookings,
            Membership = membership
        };

        return View(vm);
    }
}