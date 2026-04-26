using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Bookings;
using Microsoft.AspNetCore.Authorization;
using Application.Bookings.Commands;

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
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        return View(bookings);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int gymClassId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new CreateBookingCommand(userId, gymClassId);
        var result = await _bookingService.BookClassAsync(command);



        if (!result.IsSuccess)
        {
            TempData["Error"] = result.ErrorMessage;
            return RedirectToAction("MyBookings");
        }

        TempData["Success"] = "Booking successful!";
        return RedirectToAction("MyBookings");
    }
}