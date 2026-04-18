using Application.Memberships;
using Application.Memberships.Commands;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels; // Se till att denna using finns
                                      //


namespace Presentation.WebApp.Controllers;public class MembershipsController(IMembershipService membershipService) : Controller

{

// GET: /Memberships

public async Task<IActionResult> Index(CancellationToken ct)

{

    // Hämta listan från din Application Service

    var memberships = await membershipService.GetMembershipsAsync(ct);



    // Skapar ViewModel och fyll den med data (inklusive CTA:n)

    var viewModel = new MembershipViewModel

    {

        Memberships = memberships,

        CtaTitle = "Get Your Membership",

        CtaDescription = "Our memberships give you access to all equipment, personal training.",

        CtaPhoneNumber = "(+46) 8 410 521 00",

        CtaButtonText = "Call Us Today"

    };



    // Skicka ViewModel till vyn istället för bara listan

    return View(viewModel);

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

    catch (Exception ex) // Tips: Fånga även dina egna DomainExceptions här sen

    {

        ModelState.AddModelError("", ex.Message);

        return View(command);

    }

}

}

