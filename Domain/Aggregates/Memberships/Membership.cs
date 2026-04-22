namespace Domain.Aggregates.Memberships;

public sealed class Membership
{
    // 1. Nu som int för att matcha databasen
    public int Id { get; private set; }

    // 2. UserId ska också vara int eftersom Identity-användaren är int nu
    public int UserId { get; private set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> Benefits { get; set; } = new();
    public decimal Price { get; private set; }
    public int MonthlyClasses { get; private set; }

    // Konstruktorn tar nu int userId istället för string id
    private Membership(int userId, string title, string description, List<string> benefits, decimal price, int monthlyClasses)
    {
        UserId = userId;
        Title = Required(title, nameof(Title));
        Description = Required(description, nameof(Description));
        Benefits = benefits;
        Price = CheckPriceValue(price, nameof(Price));
        MonthlyClasses = monthlyClasses;
    }

    
    private Membership() { }

    private static string Required(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"The {propertyName} is required.");

        return value.Trim();
    }

    private static decimal CheckPriceValue(decimal value, string propertyName)
    {
        if (value < 0)
            throw new ArgumentException($"The {propertyName} must be a positive value.");

        return value;
    }

    public static Membership Create(int userId, string title, string description, List<string> benefits, decimal price = 0, int monthlyClasses = 0) =>
        new(userId, title, description, benefits, price, monthlyClasses);

    public void Update(string title, string description, List<string> benefits, decimal price, int monthlyClasses)
    {
        Title = Required(title, nameof(Title));
        Description = Required(description, nameof(Description));
        Benefits = benefits;
        Price = CheckPriceValue(price, nameof(Price));
        MonthlyClasses = monthlyClasses;
    }
}