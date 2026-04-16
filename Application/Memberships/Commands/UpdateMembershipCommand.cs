namespace Application.Memberships.Commands;

public record UpdateMembershipCommand(
    string Id,
    string Title,
    string Description,
    List<string> Benefits,
    decimal Price,
    int MonthlyClasses);