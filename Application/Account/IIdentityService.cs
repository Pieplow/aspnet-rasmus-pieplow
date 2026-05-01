using Application.Account.Commands;
using Application.Account.Responses;
using System.Security.Claims;

namespace Application.Account;

    public interface IIdentityService
    {
        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default);

        Task<bool> LoginAsync(string email, string password);

        Task LogoutAsync();
        Task<MyAccountResponse> GetMyAccountAsync(string userId);
        Task UpdateProfileAsync(ClaimsPrincipal userPrincipal, string firstName, string lastName, string phoneNumber);
        Task DeleteCurrentUserAsync(ClaimsPrincipal userPrincipal);

}
