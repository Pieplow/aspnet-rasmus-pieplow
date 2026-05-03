using Domain.Aggregates.GymClasses;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Extensions;
using Domain.Abstractions.Repositories;

namespace Infrastructure.Repositories;

public class GymClassRepository(DataContext context)
    : RepositoryBase<GymClass, int, GymClassEntity, DataContext>(context), IGymClassRepository
{
    protected override int GetId(GymClass model) => model.Id;

    protected override GymClassEntity ToEntity(GymClass model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Trainer = model.Trainer,
        StartTime = model.StartTime,
        MaxCapacity = model.MaxCapacity
    };

    protected override GymClass ToDomainModel(GymClassEntity entity)
        => new GymClass(entity.Name, entity.Trainer, entity.StartTime, entity.MaxCapacity)
        {
            
        };

    protected override void ApplyPropertyUpdated(GymClassEntity entity, GymClass model)
    {
        entity.Name = model.Name;
        entity.Trainer = model.Trainer;
        entity.StartTime = model.StartTime;
        entity.MaxCapacity = model.MaxCapacity;
    }

    public async Task UpdateAsync(GymClass gymClass, CancellationToken ct = default)
    {
        await base.UpdateAsync(gymClass, ct);
    }
}