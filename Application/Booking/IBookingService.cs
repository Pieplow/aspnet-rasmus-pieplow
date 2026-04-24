using Application.Booking.Responses;
using Domain.Abstractions;

namespace Application.Abstractions;

public interface IBookingService
{
    // För att boka ett pass
    Task<Result> BookClassAsync(int userId, int gymClassId);

    // För att avboka (den du precis skrev koden för!)
    Task<Result> CancelBookingAsync(int bookingId, int userId);

    // För att hämta användarens bokningar till "Min Sida"
    Task<IEnumerable<BookingResponse>> GetUserBookingsAsync(int userId);
}