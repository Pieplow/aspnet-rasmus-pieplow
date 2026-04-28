namespace Application.Bookings.Responses;

public record BookingResponse(
    Guid Id,
    int GymClassId,
    DateTime BookedAt
);