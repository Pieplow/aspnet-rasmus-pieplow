using Domain.Abstractions;

public record DeleteBookingCommand(int BookingId, string UserId);

