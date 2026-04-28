namespace Infrastructure.Persistence.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public int GymClassId { get; set; }
    public DateTime BookedAt { get; set; }
}