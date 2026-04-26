using Domain.Abstractions;

public record DeleteBookingCommand(int BookingId, Guid UserId);

