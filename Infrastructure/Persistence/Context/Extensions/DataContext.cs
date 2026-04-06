using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Enteties;

namespace Infrastructure.Persistence.Context.Extensions;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public DbSet<MembershipEntity> Memberships => Set<MembershipEntity>();

}
