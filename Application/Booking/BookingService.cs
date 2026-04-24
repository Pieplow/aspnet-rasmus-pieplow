using Domain.Aggregates.Bookings;     
using Domain.Abstractions.Repositories; 
using Domain.Abstractions;              

namespace Application.Booking;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Den här konstruktorn tar bort det röda på _bookingRepository och _unitOfWork
    public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CancelBookingAsync(int bookingId, int userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return Result.Failure("Booking not found.");

        // Säkerhetskontroll: Man får bara avboka sina egna pass!
        if (booking.UserId != userId)
            return Result.Failure("You are not authorized to cancel this booking.");

        await _bookingRepository.RemoveAsync(booking);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    
    public async Task<Result> BookClassAsync(int userId, int gymClassId)
    {
        var exists = await _bookingRepository.ExistsAsync(userId, gymClassId);
        if (exists) return Result.Failure("Already booked.");

        var booking = Domain.Aggregates.Bookings.Booking.Create(userId, gymClassId);
        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}