using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models.Booking;

namespace Presentation.WebApp.Controllers;

public class BookingController : Controller
{
    [HttpGet("/booking")]
    public IActionResult Index()
    {
        var viewModel = new BookingViewModel();
        return View(viewModel);
    }
}