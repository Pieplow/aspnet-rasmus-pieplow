namespace Domain.Aggregates.Bookings;

public sealed class Booking
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public int GymClassId { get; private set; }
    public DateTime BookedAt { get; private set; }

    private Booking() { } // EF Core

    private Booking(Guid userId, int gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = DateTime.UtcNow;
    }

    
    internal Booking(int id, Guid userId, int gymClassId, DateTime bookedAt)
    {
        Id = id;
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = bookedAt;
    }

    public static Booking Create(Guid userId, int gymClassId)
    {
        return new Booking(userId, gymClassId);
    }
}