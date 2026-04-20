using Application.Memberships.Commands;
using Application.Memberships.Responses;

namespace Application.Memberships;

public interface IMembershipService
{
    // Returnera din Response-modell istället för Domän-modellen
    Task<IReadOnlyList<MembershipResponse>> GetMembershipsAsync(CancellationToken ct = default);

    
    Task CreateMembershipAsync(CreateMembershipCommand command, CancellationToken ct = default);
}