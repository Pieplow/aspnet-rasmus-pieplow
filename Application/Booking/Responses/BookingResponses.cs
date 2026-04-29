namespace Application.Bookings.Responses;

public record BookingResponse(
    Guid Id,
    int GymClassId,
    string GymClassName,
    string Trainer,
    DateTime StartTime,
    DateTime BookedAt
);