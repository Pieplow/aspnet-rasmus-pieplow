namespace Application.GymClasses.Responses;

public record GymClassResponse(
    int Id,             
    string Name,
    string Trainer,
    DateTime StartTime,
    int MaxCapacity,
    int CurrentBookings
);