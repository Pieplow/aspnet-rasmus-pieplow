namespace Application.Bookings.Commands;

public record CreateBookingCommand(string UserId, int GymClassId);