namespace Domain.Aggregates.Memberships;

public sealed class Membership
{
    public int Id { get; private set; }

    public string UserId { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public List<string> Benefits { get; private set; } = new();
    public decimal Price { get; private set; }
    public int MonthlyClasses { get; private set; }

    private Membership() { }

    private Membership(
        string userId,
        string title,
        string description,
        List<string> benefits,
        decimal price,
        int monthlyClasses)
    {
        UserId = userId;
        Title = Required(title, nameof(Title));
        Description = Required(description, nameof(Description));
        Benefits = benefits ?? new List<string>();
        Price = CheckPriceValue(price, nameof(Price));
        MonthlyClasses = monthlyClasses;
    }

    public static Membership Create(
        string userId,
        string title,
        string description,
        List<string> benefits,
        decimal price,
        int monthlyClasses)
        => new(userId, title, description, benefits, price, monthlyClasses);

    public void Update(
        string title,
        string description,
        List<string> benefits,
        decimal price,
        int monthlyClasses)
    {
        Title = Required(title, nameof(Title));
        Description = Required(description, nameof(Description));
        Benefits = benefits ?? new List<string>();
        Price = CheckPriceValue(price, nameof(Price));
        MonthlyClasses = monthlyClasses;
    }

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
}