using Domain.Aggregates.Bookings;
using Domain.Abstractions.Repositories;
using Domain.Abstractions;       // För att hitta Result
using Application.Abstractions;



namespace Application.Booking;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    // Vi tar bort unitOfWork eftersom den inte finns i din bild!
    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Result> CancelBookingAsync(int bookingId, int userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return Result.Failure("Booking not found.");

        if (booking.UserId != userId)
            return Result.Failure("You are not authorized to cancel this booking.");

        await _bookingRepository.RemoveAsync(booking);

        // Eftersom du inte har UnitOfWork, sparar din RepositoryBase 
        // automatiskt (jag såg _context.SaveChangesAsync i din bas tidigare).

        return Result.Success();
    }
}