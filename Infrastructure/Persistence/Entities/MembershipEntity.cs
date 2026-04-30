namespace Infrastructure.Persistence.Entities;

public sealed class MembershipEntity
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!; 

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int MonthlyClasses { get; set; }

    

   
    public ICollection<MembershipBenefitEntity> Benefits { get; set; } = new List<MembershipBenefitEntity>();
}