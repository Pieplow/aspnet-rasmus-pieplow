namespace Infrastructure.Persistence.Entities;

public class MembershipBenefitEntity
{
    public int Id { get; set; }
    public string Benefit { get; set; } = null!;

  
    public int MembershipId { get; set; }

    public MembershipEntity Membership { get; set; } = null!;
}