using Application.Account;
using Application.Account.Commands;
using Application.Account.Responses;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return new RegisterUserResponse(false, null, ["E-post måste anges."]);

        if (string.IsNullOrWhiteSpace(command.Password))
            return new RegisterUserResponse(false, null, ["Lösenord måste anges."]);

        var existingUser = await userManager.FindByEmailAsync(command.Email);

        if (existingUser is not null)
            return new RegisterUserResponse(false, null, ["En anvädnare med samma e-post existerar redan."]);

        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
            return new RegisterUserResponse(true, user.Id, []);

        var errors = result.Errors.Select(e => e.Description).ToArray();

        return new RegisterUserResponse(false, null, errors);
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return false;

        var result = await signInManager.PasswordSignInAsync(
            email,
            password,
            isPersistent: false,
            lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}