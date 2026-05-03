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

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Trainer { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentBookings { get; set; }

    
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