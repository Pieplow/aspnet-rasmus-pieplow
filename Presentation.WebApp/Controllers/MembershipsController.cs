using Application.Memberships;
using Application.Memberships.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class MembershipsController(IMembershipService membershipService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        // Hämta DTOs från Application
        var memberships = await membershipService.GetMembershipsAsync(ct);
        return View(memberships);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMembershipCommand command, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(command);

        try
        {
            await membershipService.CreateMembershipAsync(command, ct);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            // Fångar valideringsfelen från din Membership-entitets Required/CheckPriceValue
            ModelState.AddModelError("", ex.Message);
            return View(command);
        }
    }
}