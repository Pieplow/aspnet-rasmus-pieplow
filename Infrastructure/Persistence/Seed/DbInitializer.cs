using Domain.Aggregates.GymClasses;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class DbInitializer
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        // SEED MEMBERSHIPS
        modelBuilder.Entity<MembershipEntity>().HasData(
            new MembershipEntity
            {
                Id = 1,
                UserId = "seed-user-silver", // LÄGG TILL DENNA
                Title = "Silver",
                Price = 395,
                Description = "Basic membership"
            },
            new MembershipEntity
            {
                Id = 2,
                UserId = "seed-user-gold", // LÄGG TILL DENNA
                Title = "Gold",
                Price = 595,
                Description = "Premium membership"
            }
        );

        // SEED BENEFITS
        modelBuilder.Entity<MembershipBenefitEntity>().HasData(
            new MembershipBenefitEntity { Id = 1, MembershipId = 1, Benefit = "Standard Locker" },
            new MembershipBenefitEntity { Id = 2, MembershipId = 1, Benefit = "High-energy group fitness" },
            new MembershipBenefitEntity { Id = 3, MembershipId = 2, Benefit = "Priority Support" },
            new MembershipBenefitEntity { Id = 4, MembershipId = 2, Benefit = "Premium Locker" }
        );

        modelBuilder.Entity<GymClass>().HasData(
         new
         {
             Id = 1,
             Name = "Yoga Flow",
             Trainer = "Maria",

             StartTime = new DateTime(2026, 05, 01, 08, 00, 00),
             CurrentBookings = 0,
             MaxCapacity = 20
         },
         new
         {
             Id = 2,
             Name = "Crossfit",
             Trainer = "Erik",
             StartTime = new DateTime(2026, 05, 01, 17, 00, 00),
             CurrentBookings = 0,
             MaxCapacity = 15
         }
     );
    }
}