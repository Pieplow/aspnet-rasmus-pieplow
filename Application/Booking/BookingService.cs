using Application.Bookings.Commands;
using Application.Bookings.Responses;
using Domain.Abstractions;
using Domain.Aggregates.Bookings;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Bookings;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    private readonly IMemoryCache _cache;

    public BookingService(IBookingRepository bookingRepository, IMemoryCache cache)
    {
        _bookingRepository = bookingRepository;
        _cache = cache;
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

    public async Task<List<BookingResponse>> GetUserBookingsAsync(string userId)
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

    public async Task<Result> CancelBookingAsync(Guid bookingId, string userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return Result.Failure("Not found");

        if (booking.UserId != userId)
            return Result.Failure("Not allowed");

        await _bookingRepository.RemoveAsync(booking);

        
        _cache.Remove($"bookings_{userId}");

        return Result.Success();
    }
}