using Domain.Aggregates.GymClasses;

namespace Domain.Abstractions.Repositories;

public interface IGymClassRepository : IRepositoryBase<GymClass, int>
{
    new Task<bool> UpdateAsync(GymClass gymClass, CancellationToken ct = default);
}