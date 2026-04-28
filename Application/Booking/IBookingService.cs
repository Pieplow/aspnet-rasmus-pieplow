using Application.Bookings.Commands;
using Application.Bookings.Responses;
using Domain.Abstractions;

public interface IBookingService
{
    Task<Result> BookClassAsync(CreateBookingCommand command);

    Task<List<BookingResponse>> GetUserBookingsAsync(string userId);

    Task<Result> CancelBookingAsync(Guid bookingId, string userId);
}