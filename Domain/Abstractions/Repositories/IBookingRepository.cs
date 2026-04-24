using Domain.Abstractions.Repositories;
using Domain.Aggregates.Bookings;

public interface IBookingRepository : IRepositoryBase<Booking, int>
{
    Task<bool> ExistsAsync(int userId, int gymClassId, CancellationToken ct = default);

    Task<int> CountBookingsForClassAsync(int gymClassId, CancellationToken ct = default);
}