using Application.Bookings.Commands;
using Application.Bookings.Responses;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Aggregates.Bookings;

namespace Application.Bookings;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Result> BookClassAsync(CreateBookingCommand command)
    {
        var alreadyBooked = await _bookingRepository.ExistsAsync(command.UserId, command.GymClassId);

        if (alreadyBooked)
            return Result.Failure("Du är redan bokad.");

        var booking = Booking.Create(command.UserId, command.GymClassId);

        await _bookingRepository.AddAsync(booking);

        return Result.Success();
    }

    public async Task<List<BookingResponse>> GetUserBookingsAsync(int userId)
    {
        var all = await _bookingRepository.GetAllAsync();

        return all
            .Where(b => b.UserId == userId)
            .Select(b => new BookingResponse(
                b.Id,
                b.GymClassId,
                b.BookedAt
            ))
            .ToList();
    }

    public async Task<Result> CancelBookingAsync(int bookingId, int userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return Result.Failure("Not found");

        if (booking.UserId != userId)
            return Result.Failure("Not allowed");

        await _bookingRepository.RemoveAsync(booking);

        return Result.Success();
    }
}