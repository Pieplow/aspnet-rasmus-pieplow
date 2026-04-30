namespace Application.Memberships.Commands;

public record CreateMembershipCommand(
    string UserId, 
    string Title,
    string Description,
    List<string> Benefits,
    decimal Price,
    int MonthlyClasses);