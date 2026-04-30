using Domain.Abstractions.Repositories;
using Domain.Aggregates.Memberships;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Repositories;

public sealed class MembershipRepository(DataContext context)
    : RepositoryBase<Membership, int, MembershipEntity, DataContext>(context)
    , IMembershipRepository
{
    protected override void ApplyPropertyUpdated(MembershipEntity entity, Membership model)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.Price = model.Price;
        entity.MonthlyClasses = model.MonthlyClasses;
        
    }

    protected override int GetId(Membership model)
    {
        
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {
        var benefits = entity.Benefits.Select(b => b.Benefit).ToList();

        var model = Membership.Create(
            entity.UserId,
            entity.Title,
            entity.Description,
            benefits,
            entity.Price,
            entity.MonthlyClasses
        );

        return model;
    }

    protected override MembershipEntity ToEntity(Membership model)
    {
        var entity = new MembershipEntity
        {
            Id = model.Id, 
            UserId = model.UserId,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses
        };
        return entity;
    }
}