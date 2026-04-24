namespace Infrastructure.Persistence.Entities;

public class BookingEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GymClassId { get; set; }
    public DateTime BookedAt { get; set; }
}