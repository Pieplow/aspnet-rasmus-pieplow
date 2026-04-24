namespace Application.Bookings.Responses;

public record BookingResponse(
    int Id,
    int GymClassId,
    DateTime BookedAt
);