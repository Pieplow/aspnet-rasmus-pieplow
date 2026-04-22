namespace Infrastructure.Persistence.Entities;

public sealed class MembershipEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!; 

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int MonthlyClasses { get; set; }

    

   
    public ICollection<MembershipBenefitEntity> Benefits { get; set; } = new List<MembershipBenefitEntity>();
}