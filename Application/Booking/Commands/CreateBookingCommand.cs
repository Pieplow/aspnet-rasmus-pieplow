namespace Application.Bookings.Commands;

public record CreateBookingCommand(Guid UserId, int GymClassId);