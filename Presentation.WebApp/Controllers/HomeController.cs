using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

     
        [HttpPost]
        public IActionResult Contact(string firstName, string lastName, string email, string message)
        {
            TempData["Success"] = "Message sent!";
            return RedirectToAction("Contact");
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
