using Application.Bookings.Commands;
using Application.Bookings.Responses;
using Domain.Abstractions;

public interface IBookingService
{
    Task<Result> BookClassAsync(CreateBookingCommand command);

    Task<List<BookingResponse>> GetUserBookingsAsync(Guid userId);

    Task<Result> CancelBookingAsync(int bookingId, Guid userId);
}