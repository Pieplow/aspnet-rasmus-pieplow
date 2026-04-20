using Application.Account.Commands;
using Application.Account.Responses;

namespace Application.Account;

public interface IIdentityService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default);
}