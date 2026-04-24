using Domain.Aggregates.Bookings;

namespace Domain.Abstractions.Repositories;

public interface IBookingRepository : IRepositoryBase<Booking, int>
{

    Task<bool> ExistsAsync(int userId, int gymClassId);
}