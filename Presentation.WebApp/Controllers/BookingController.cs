using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Abstractions; // Denna behövs för IBookingService
using Microsoft.AspNetCore.Authorization;

namespace Presentation.WebApp.Controllers;

[Authorize] // Gör att man måste vara inloggad för att boka/avboka
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    // Konstruktorn gör att controllern kan använda din service
    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // Denna visar "Mina bokningar"
    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        return View(bookings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int bookingId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _bookingService.CancelBookingAsync(bookingId, userId);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.ErrorMessage;
            return RedirectToAction("MyBookings");
        }

        TempData["Success"] = "Your booking has been cancelled.";
        return RedirectToAction("MyBookings");
    }
}