using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class MembershipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
