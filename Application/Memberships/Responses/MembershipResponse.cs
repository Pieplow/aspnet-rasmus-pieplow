namespace Application.Memberships.Responses;

public record MembershipResponse(
    int Id,
    string Title,
    string Description,
    List<string> Benefits,
    decimal Price,
    int MonthlyClasses);