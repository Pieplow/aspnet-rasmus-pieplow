using Domain.Aggregates.Bookings;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : RepositoryBase<Booking, Guid, BookingEntity, DataContext>, IBookingRepository
{
    public BookingRepository(DataContext context) : base(context) { }

   
    protected override Guid GetId(Booking model) => model.Id;

    
    protected override BookingEntity ToEntity(Booking model) => new()
    {
        Id = model.Id,
        UserId = model.UserId,
        GymClassId = model.GymClassId,
        BookedAt = model.BookedAt
    };


    protected override Booking ToDomainModel(BookingEntity entity)
    => Booking.Rehydrate(entity.Id, entity.UserId, entity.GymClassId, entity.BookedAt);


    protected override void ApplyPropertyUpdated(BookingEntity entity, Booking model)
    {
        entity.UserId = model.UserId;
        entity.GymClassId = model.GymClassId;
    }

    public async Task<bool> ExistsAsync(string userId, int gymClassId, CancellationToken ct = default)
    {
        return await Set.AnyAsync(b => b.UserId == userId && b.GymClassId == gymClassId, ct);
    }

    public async Task<int> CountBookingsForClassAsync(int gymClassId, CancellationToken ct = default)
    {
        return await Set.CountAsync(b => b.GymClassId == gymClassId, ct);
    }

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
    {
        // 1. Hämta databas-entiteterna (BookingEntity) från tabellen
        var entities = await _context.Bookings
            .Where(b => b.UserId == userId)
            .ToListAsync();

        // 2. Använd Rehydrate för att skapa domänobjekt av datan
        // Vi mappar fälten från entiteten (e) till Rehydrate-metoden
        return entities.Select(e => Booking.Rehydrate(
            e.Id,
            e.UserId,
            e.GymClassId,
            e.BookedAt));
    }
}