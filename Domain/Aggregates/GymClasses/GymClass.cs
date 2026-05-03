namespace Domain.Aggregates.GymClasses;

public class GymClass
{
    // EF Core behöver ofta en parameterlös konstruktor, även om den är privat
    private GymClass() { }

    // Denna konstruktor använder vi i vårt Repository
    public GymClass(string name, string trainer, DateTime startTime, int maxCapacity)
    {
        Name = name;
        Trainer = trainer;
        StartTime = startTime;
        MaxCapacity = maxCapacity;
        CurrentBookings = 0; // Startar alltid på noll för nya pass
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Trainer { get; private set; } = null!;
    public DateTime StartTime { get; private set; }
    public int MaxCapacity { get; private set; }
    public int CurrentBookings { get; private set; }

    // Logik för att kontrollera status
    public bool CanBook() => CurrentBookings < MaxCapacity;
    public int AvailableSlots => MaxCapacity - CurrentBookings;

    // DDD-metod: Utför en bokning
    public void Book()
    {
        if (!CanBook())
            throw new InvalidOperationException("Passet är tyvärr fullbokat.");

        CurrentBookings++;
    }

    // DDD-metod: Hantera avbokning
    public void Cancel()
    {
        if (CurrentBookings > 0)
            CurrentBookings--;
    }
}