namespace Domain.Aggregates.Bookings;

public sealed class Booking
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int GymClassId { get; private set; }
    public DateTime BookedAt { get; private set; }

    private Booking() { } // EF Core

    private Booking(int userId, int gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = DateTime.UtcNow;
    }

    
    internal Booking(int id, int userId, int gymClassId, DateTime bookedAt)
    {
        Id = id;
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = bookedAt;
    }

    public static Booking Create(int userId, int gymClassId)
    {
        return new Booking(userId, gymClassId);
    }
}