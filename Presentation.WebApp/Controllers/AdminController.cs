using Application.GymClasses;
using Application.GymClasses.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

[Authorize(Roles = "Admin")]
// FIX 1: Injicera IGymClassService istället för IBookingService
public class AdminController(IGymClassService gymClassService) : Controller
{
    // 1. VISA DASHBOARD
    public async Task<IActionResult> Index()
    {
        // FIX 2: Använd GetAllAsync() från din nya service
        var classes = await gymClassService.GetAllAsync();
        return View(classes);
    }

    // 2. SKAPA NYTT PASS (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    // FIX 3: Ändra GymClassDTO till GymClassResponse
    public async Task<IActionResult> Create(GymClassResponse model)
    {
        if (!ModelState.IsValid)
        {
            var classes = await gymClassService.GetAllAsync();
            return View("Index", classes);
        }

        // FIX 4: Använd CreateAsync (om du lagt till den i servicen, annars lämna tom tills vidare)
        // await gymClassService.CreateAsync(model);

        TempData["Success"] = "Created Session Successfully!";
        return RedirectToAction(nameof(Index));
    }

    // 3. RADERA PASS (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        // FIX 5: Använd DeleteAsync(id) som vi precis fixade i GymClassService
        await gymClassService.DeleteAsync(id);

        TempData["Success"] = "Deleted Session Successfully!";
        return RedirectToAction(nameof(Index));
    }
}