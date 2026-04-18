using Application.Memberships;
using Application.Memberships.Commands;
using Microsoft.AspNetCore.Mvc;

[ApiController] // <-- GÖR ATT SWAGGER HITTAR DEN
[Route("api/[controller]")] // <-- ADRESSEN BLIR api/memberships
public class MembershipsAPIController(IMembershipService membershipService) : ControllerBase // <-- Ändra till ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMemberships(CancellationToken ct)
    {
        var memberships = await membershipService.GetMembershipsAsync(ct);
        return Ok(memberships); // Returnerar JSON istället för en HTML-vy
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMembershipCommand command, CancellationToken ct)
    {
        // [FromBody] är viktigt för att Postman ska kunna skicka JSON-data
        await membershipService.CreateMembershipAsync(command, ct);
        return Ok(new { message = "Membership created in CoreFitness database!" });
    }
}