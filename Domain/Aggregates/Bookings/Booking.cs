namespace Domain.Aggregates.Bookings;

public sealed class Booking
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = null!;
    public int GymClassId { get; private set; }
    public DateTime BookedAt { get; private set; }

    private Booking() { } // EF Core

    private Booking(string userId, int gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = DateTime.UtcNow;
    }

    
    internal Booking(Guid id, string userId, int gymClassId, DateTime bookedAt)
    {
        Id = id;
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = bookedAt;
    }

    public static Booking Create(string userId, int gymClassId)
    {
        return new Booking(userId, gymClassId);
    }
}