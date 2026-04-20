using Application.Account;
using Application.Account.Commands;
using Application.Account.Responses;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        // 1. Skapa entiteten
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email
        };

        // 2. Försök spara i SQL Server via Identity
        var result = await userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            return new RegisterUserResponse(true, user.Id, Array.Empty<string>());
        }

        // 3. Om det misslyckas (t.ex. för svagt lösenord), mappa felen
        var errors = result.Errors.Select(e => e.Description).ToArray();
        return new RegisterUserResponse(false, null, errors);
    }

    
}