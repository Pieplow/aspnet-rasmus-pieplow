using Application.Bookings.Commands;
using Application.Bookings.Responses;
using Domain.Abstractions;

public interface IBookingService
{
    Task<Result> BookClassAsync(CreateBookingCommand command);

    Task<List<BookingResponse>> GetUserBookingsAsync(int userId);

    Task<Result> CancelBookingAsync(int bookingId, int userId);
}