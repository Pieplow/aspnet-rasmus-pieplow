using Domain.Abstractions.Repositories;
using Domain.Aggregates.GymClasses; // Denna används för returtypen
using Infrastructure.Persistence.Context; // Där din DataContext bor
using Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore; // KRÄVS för ToListAsync()

namespace Infrastructure.Repositories;

// Om IGymClassRepository kräver Domain-modeller, måste vi mappa dem här
public class GymClassRepository(DataContext context) : IGymClassRepository
{
    public async Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct)
    {
        // 1. Hämta från databasen (Entities)
        // 2. Om IGymClassRepository förväntar sig Domain-modeller måste de konverteras.
        // Här antar vi att tabellen i DataContext heter 'GymClasses'

        var entities = await context.GymClasses.ToListAsync(ct);

        // Mappa om från Infrastructure-Entity till Domain-Aggregate
        return entities.Select(e => new GymClass
        {
            Id = e.Id,
            Name = e.Name,
            Trainer = e.Trainer,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Intensity = e.Intensity
        });
    }
}