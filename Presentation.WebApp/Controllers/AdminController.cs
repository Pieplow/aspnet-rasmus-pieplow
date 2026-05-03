using Application.GymClasses;
using Application.GymClasses.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

[Authorize(Roles = "Admin")]

public class AdminController(IGymClassService gymClassService) : Controller
{
    
    public async Task<IActionResult> Index()
    {
        
        var classes = await gymClassService.GetAllAsync();
        return View(classes);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
  
    public async Task<IActionResult> Create(GymClassResponse model)
    {
        if (!ModelState.IsValid)
        {
            var classes = await gymClassService.GetAllAsync();
            return View("Index", classes);
        }
        await gymClassService.CreateAsync(model);

        TempData["Success"] = "Created Session Successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("/Admin/Delete/{id}")] // Tvingar URL-strukturen
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await gymClassService.DeleteAsync(id);
        TempData["Success"] = "Deleted Session Successfully!";
        return RedirectToAction(nameof(Index));
    }


}