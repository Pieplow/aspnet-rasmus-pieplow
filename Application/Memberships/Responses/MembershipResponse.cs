namespace Application.Memberships.Responses;

public record MembershipResponse(
    string Id,
    string Title,
    string Description,
    List<string> Benefits,
    decimal Price,
    int MonthlyClasses);