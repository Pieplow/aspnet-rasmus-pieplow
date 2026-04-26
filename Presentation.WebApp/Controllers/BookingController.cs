using Application.Bookings;
using Application.Bookings.Commands;
using Domain.Abstractions.Repositories; // Lägg till denna för interfacet
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IGymClassRepository _gymClassRepository; // Lägg till detta fält

    // Uppdatera konstruktorn att ta emot båda
    public BookingController(
        IBookingService bookingService,
        IGymClassRepository gymClassRepository)
    {
        _bookingService = bookingService;
        _gymClassRepository = gymClassRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Schedule()
    {
        // Nu fungerar detta anropet!
        var classes = await _gymClassRepository.GetAllAsync();
        return View(classes);
    }

    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(claim))
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = Guid.Parse(claim);
        var bookings = await _bookingService.GetUserBookingsAsync(userId);

        return View(bookings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int gymClassId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claim)) return Unauthorized();

        var userId = Guid.Parse(claim);
        var command = new CreateBookingCommand(userId, gymClassId);
        var result = await _bookingService.BookClassAsync(command);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.ErrorMessage;
            return RedirectToAction("Schedule"); // Redirecta till schemat om bokning misslyckas
        }

        TempData["Success"] = "Bokningen genomförd!";
        return RedirectToAction("MyBookings");
    }
}