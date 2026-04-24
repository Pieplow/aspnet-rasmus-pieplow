using Microsoft.AspNetCore.Mvc;
using Application.Bookings;
using Application.Bookings.Commands;

namespace Presentation.WebApp.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingApiController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingApiController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Book(CreateBookingCommand command)
    {
        var result = await _bookingService.BookClassAsync(command);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok();
    }
}