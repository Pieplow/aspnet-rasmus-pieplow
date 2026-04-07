using Application.Memberships;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models.Memberships;

namespace Presentation.WebApp.Controllers;

public class MembershipsController(IMembershipService service) : Controller
{
    public async Task<IActionResult> Index()
    {
        var memberships = await service.GetMembershipsAsync();

        var viewModel = new MembershipViewModel()
        {
            Memberships = memberships
        };

        return View(viewModel);
    }
}

