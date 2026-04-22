namespace Application.Memberships.Commands;

public record CreateMembershipCommand(
    int UserId, 
    string Title,
    string Description,
    List<string> Benefits,
    decimal Price,
    int MonthlyClasses);