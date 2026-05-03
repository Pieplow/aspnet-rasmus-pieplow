using Domain.Abstractions.Repositories;
using Domain.Aggregates.Bookings;

public interface IBookingRepository : IRepositoryBase<Booking, Guid>
{
    Task<bool> ExistsAsync(string userId, int gymClassId, CancellationToken ct = default);

    Task<int> CountBookingsForClassAsync(int gymClassId, CancellationToken ct = default);
    Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);

}