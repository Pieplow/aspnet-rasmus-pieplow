
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class MembershipConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasIndex(x => x.UserId);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.MonthlyClasses)
            .IsRequired();


    }
}

internal class MembershipBenefitConfiguration : IEntityTypeConfiguration<MembershipBenefitEntity>
{
    public void Configure(EntityTypeBuilder<MembershipBenefitEntity> builder)
    {
         
        builder.ToTable("MembershipBenefits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MembershipId)
            .IsRequired();

        builder.Property(x => x.Benefit)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.MembershipId);

        builder.HasOne(x => x.Membership)
            .WithMany(x => x.Benefits)
            .HasForeignKey(x => x.MembershipId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
