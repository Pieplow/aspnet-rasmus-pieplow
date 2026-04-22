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
        // Här kan du även hantera Benefits om det behövs
    }

    protected override int GetId(Membership model)
    {
        // Nu när model.Id är en int, returnerar vi den direkt!
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {
        var benefits = entity.Benefits.Select(b => b.Benefit).ToList();

        // Vi använder den nya Create-metoden som tar int (UserId och ev. Id)
        // OBS: Om din Create-metod kräver UserId, se till att entity har det fältet
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
            Id = model.Id, // Ingen int.Parse behövs längre!
            UserId = model.UserId,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses
        };
        return entity;
    }
}