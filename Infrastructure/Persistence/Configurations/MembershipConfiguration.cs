using Infrastructure.Persistence.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class MembershipConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
        throw new NotImplementedException();
    }
}
