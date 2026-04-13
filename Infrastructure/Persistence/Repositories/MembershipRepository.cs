using Domain.Abstractions.Repositories;
using Domain.Aggregates.Memberships;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Repositories;

public sealed class MembershipRepository(DataContext context)
    : RepositoryBase<Membership, string, MembershipEntity, DataContext>(context)
    , IMembershipRepository
{
    //configuration for the repository, not implemented yet//
    protected override void ApplyPropertyUpdated(MembershipEntity entity, Membership model)
    {
        throw new NotImplementedException();
    }

    protected override string GetId(Membership model)
    {
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {

        var benefits = new List<string>();
        foreach (var benefit in entity.Benefits)
            benefits.Add(benefit.Benefit);


        var model = Membership.Create(
            entity.Id,
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
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses
        };
        return entity;
    }
}
