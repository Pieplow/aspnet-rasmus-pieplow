namespace Domain.Aggregates.Bookings;

public sealed class Booking
{
    public int Id { get; private set; }
    public int UserId { get; private set; } 
    public int GymClassId { get; private set; } 
    public DateTime BookedAt { get; private set; }

    private Booking(int userId, int gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
        BookedAt = DateTime.UtcNow;
    }

    public static Booking Create(int userId, int gymClassId) => new(userId, gymClassId);

    
    private Booking() { }
}