using Domain.Aggregates.GymClasses;

namespace Domain.Abstractions.Repositories;

public interface IGymClassRepository
{
    Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct = default);
    Task<GymClass?> GetByIdAsync(int id, CancellationToken ct = default);
    Task UpdateAsync(GymClass gymClass, CancellationToken ct = default);
}