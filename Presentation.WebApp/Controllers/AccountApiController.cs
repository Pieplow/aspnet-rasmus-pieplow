using Application.Account;
using Application.Account.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountApiController(IIdentityService identityService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        
        var result = await identityService.RegisterUserAsync(command);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors });
        }

        
        return Ok(new { userId = result.UserId });
    }
}