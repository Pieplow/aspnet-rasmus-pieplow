using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Entities;

public sealed class MembershipBenefitEntity
{
    public string Id { get; set; } = null!;
    public string MembershipId { get; set; } = null!;
    public string Benefit { get; set; } = null!;

    public MembershipEntity Membership { get; set; } = null!;
}
