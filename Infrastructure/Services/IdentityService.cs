using Application.Account;
using Application.Account.Commands;
using Application.Account.Responses;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
            return new RegisterUserResponse(false, null, ["En användare med samma e-post existerar redan."]);

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

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        var result = await signInManager.PasswordSignInAsync(
            user.UserName,
            password,
            isPersistent: false,
            lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task UpdateProfileAsync(ClaimsPrincipal userPrincipal, string firstName, string lastName, string phoneNumber)
    {
        var user = await userManager.GetUserAsync(userPrincipal);

        if (user == null)
            return;

        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;

        await userManager.UpdateAsync(user);
    }

    public async Task DeleteCurrentUserAsync(ClaimsPrincipal userPrincipal)
    {
        var user = await userManager.GetUserAsync(userPrincipal);

        if (user == null)
            return;

        await userManager.DeleteAsync(user);
    }

    public async Task<MyAccountResponse> GetMyAccountAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return new MyAccountResponse();

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return new MyAccountResponse();

        return new MyAccountResponse
        {
            Email = user.Email ?? "",
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
    {
        return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    }

    public async Task<ExternalLoginInfo?> GetExternalLoginInfoAsync()
    {
        return await signInManager.GetExternalLoginInfoAsync();
    }

    public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
    {
        return await signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
    }
}