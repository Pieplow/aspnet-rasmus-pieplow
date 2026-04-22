using Domain.Abstractions.Repositories;
using Domain.Aggregates.GymClasses;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

// Den här raden (klassen) MÅSTE omsluta all kod under
public class GymClassRepository(DataContext context) : IGymClassRepository
{
    public async Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct)
    {
        // Nu kommer 'context' hittas eftersom den skickas in i klassen ovan
        var entities = await context.GymClasses.ToListAsync(ct);

        return entities.Select(e => new GymClass
        {
            Id = e.Id,
            Name = e.Name,
            Trainer = e.Trainer,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Intensity = e.Intensity
        }).ToList();
    }

    public async Task<GymClass?> GetByIdAsync(int id, CancellationToken ct)
    {
        var e = await context.GymClasses.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (e == null) return null;

        return new GymClass
        {
            Id = e.Id,
            Name = e.Name,
            Trainer = e.Trainer,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Intensity = e.Intensity
        };
    }
} 