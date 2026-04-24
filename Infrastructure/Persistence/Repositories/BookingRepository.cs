using Domain.Aggregates.Bookings;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : RepositoryBase<Booking, int, BookingEntity, DataContext>, IBookingRepository
{
    public BookingRepository(DataContext context) : base(context) { }

    // 1. Mappa ID
    protected override int GetId(Booking model) => model.Id;

    // 2. Mappa till Entity (Infrastruktur)
    protected override BookingEntity ToEntity(Booking model) => new()
    {
        Id = model.Id,
        UserId = model.UserId,
        GymClassId = model.GymClassId,
        BookedAt = model.BookedAt
    };

    // 3. Mappa till Domain (Logik)
    protected override Booking ToDomainModel(BookingEntity entity)
    => Booking.Create(entity.UserId, entity.GymClassId);

    // 4. Hantera uppdatering
    protected override void ApplyPropertyUpdated(BookingEntity entity, Booking model)
    {
        entity.UserId = model.UserId;
        entity.GymClassId = model.GymClassId;
    }

    // Din unika metod
    public async Task<bool> ExistsAsync(int userId, int gymClassId, CancellationToken ct = default)
    {
        return await Set.AnyAsync(b => b.UserId == userId && b.GymClassId == gymClassId, ct);
    }

    public async Task<int> CountBookingsForClassAsync(int gymClassId, CancellationToken ct = default)
    {
        return await Set.CountAsync(b => b.GymClassId == gymClassId, ct);
    }
}