namespace Infrastructure.Persistence.Entities;
public class Booking
{
    public Guid UserId { get; private set; }
    public Guid GymClassId { get; private set; }

    public Booking(Guid userId, Guid gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
    }
}