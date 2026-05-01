namespace Domain.Aggregates.GymClasses;

public class GymClass
{
  
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