using Infrastructure.Identity;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context.Extensions;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        DbInitializer.SeedData(modelBuilder);
    }

    public DbSet<MembershipEntity> Memberships => Set<MembershipEntity>();
    public DbSet<MembershipBenefitEntity> MembershipBenefits => Set<MembershipBenefitEntity>();
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();

    // RÄTTELSE: Vi mappar mot GymClassEntity, precis som du gjort med BookingEntity.
    // Detta fixar felet "Cannot create a DbSet for GymClassEntity".
    public DbSet<GymClassEntity> GymClasses => Set<GymClassEntity>();

    // RÄTTELSE: Om du har en GymEntity i din Entities-mapp ska den användas här.
    // Vi undviker att använda domänmodellen "Gym" direkt i DataContext.
   
}