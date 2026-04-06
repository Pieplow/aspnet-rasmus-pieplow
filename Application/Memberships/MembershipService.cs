using Domain.Abstractions.Repositories;

namespace Application.Memberships;


public sealed class MembershipService(IMembershipRepository repo) : IMembershipService
{
}

