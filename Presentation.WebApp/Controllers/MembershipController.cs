using Application.Memberships;
using Application.Memberships.Commands;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers;

public class MembershipsController(IMembershipService membershipService) : Controller

{

// GET: /Memberships

public async Task<IActionResult> Index(CancellationToken ct)

{

    // Hämta listan från din Application Service

    var memberships = await membershipService.GetMembershipsAsync(ct);



   

    var viewModel = new MembershipViewModel

    {

        Memberships = memberships,

        CtaTitle = "Get Your Membership",

        CtaDescription = "Our memberships give you access to all equipment, personal training.",

        CtaPhoneNumber = "(+46) 8 410 521 00",

        CtaButtonText = "Call Us Today"

    };



  

    return View(viewModel);

}



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        string title,
        string description,
        decimal price,
        int monthlyClasses,
        CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");


        var command = new CreateMembershipCommand(
            userId!,
            title,
            description,
            new List<string>(),
            price,
            monthlyClasses
        );

        await membershipService.CreateMembershipAsync(command, ct);

        TempData["Success"] = "Membership activated";

        return RedirectToAction("Index", "MyAccount");
    }
}



