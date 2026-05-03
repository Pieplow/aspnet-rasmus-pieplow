using Domain.Abstractions;
using Infrastructure.Persistence.Context.Extensions;

namespace Infrastructure.Persistence;

public class UnitOfWork(DataContext context) : IUnitOfWork
{
    public async Task<int> CompleteAsync(CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}