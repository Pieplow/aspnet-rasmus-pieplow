using Application.Account.Commands;
using Application.Account.Responses;

namespace Application.Account;

    public interface IIdentityService
    {
        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default);

        Task<bool> LoginAsync(string email, string password);

        Task LogoutAsync();
    }
