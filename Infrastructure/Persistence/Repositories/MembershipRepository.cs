using Domain.Abstractions.Repositories;
using Domain.Aggregates.Memberships;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Enteties;

namespace Infrastructure.Persistence.Repositories;

public sealed class MembershipRepository(DataContext context)
    : RepositoryBase<Memberships, string, MembershipEntity, DataContext>(context)
    , IMembershipRepository
{
    //configuration for the repository, not implemented yet//
    protected override void ApplyPropertyUpdated(MembershipEntity entity, Memberships model)
    {
        throw new NotImplementedException();
    }

    protected override string GetId(Memberships model)
    {
        throw new NotImplementedException();
    }

    protected override Memberships ToDomainModel(MembershipEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override MembershipEntity ToEntity(Memberships model)
    {
        throw new NotImplementedException();
    }
}
