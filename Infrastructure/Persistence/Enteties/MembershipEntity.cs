namespace Infrastructure.Persistence.Enteties;

public sealed class MembershipEntity
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
    public int MonthlyClasses { get; set; } 

    public ICollection<MembershipBenefitEntity> Benefits { get; set; } = [];

}
