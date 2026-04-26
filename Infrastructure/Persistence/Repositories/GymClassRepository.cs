using Domain.Abstractions.Repositories;
using Domain.Aggregates.GymClasses;
using Infrastructure.Persistence.Context; 
using Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GymClassRepository(DataContext context) : IGymClassRepository
{
    public async Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct)
    {
        // Ingen manuell Select/Mapping behövs! 
        // EF Core mappar direkt till Domain.GymClass
        return await context.GymClasses
            .AsNoTracking() // Gör det snabbare eftersom vi bara ska läsa datan
            .ToListAsync(ct);
    }

    public async Task<GymClass?> GetByIdAsync(int id, CancellationToken ct)
    {
        // Här hämtar vi objektet direkt. 
        // Eftersom vi ska boka på detta objektet kör vi INTE AsNoTracking här.
        return await context.GymClasses
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task UpdateAsync(GymClass gymClass, CancellationToken ct)
    {
        // Denna metod behövs för att spara ändringen när vi kört gymClass.Book()
        context.GymClasses.Update(gymClass);
        await context.SaveChangesAsync(ct);
    }
}