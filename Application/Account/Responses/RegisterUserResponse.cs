namespace Application.Account.Responses;

public record RegisterUserResponse(
    bool Succeeded,
    string? UserId,
    string[] Errors);