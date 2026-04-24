using Domain.Abstractions.Repositories;
using Domain.Aggregates.Bookings;
using Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly DataContext _context; // Här definierar vi namnet!

    public BookingRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<BookingEntity?> GetByIdAsync(int id)
    {
        // Nu fungerar _context eftersom vi definierat den ovan
        return await _context.Bookings.FindAsync(id);
    }

    public async Task RemoveAsync(BookingEntity booking)
    {
        _context.Bookings.Remove(booking);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int userId, int gymClassId)
    {
        return await _context.Bookings.AnyAsync(b => b.UserId == userId && b.GymClassId == gymClassId);
    }

    public async Task AddAsync(BookingEntity booking)
    {
        await _context.Bookings.AddAsync(booking);
    }
}