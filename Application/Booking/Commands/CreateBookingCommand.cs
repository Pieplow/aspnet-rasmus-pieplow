namespace Application.Bookings.Commands;

public record CreateBookingCommand(int UserId, int GymClassId);