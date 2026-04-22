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
                Id = 1, // Detta fungerar nu när Id är int i entiteten
                Title = "Silver", // Ändrat från Name till Title
                Price = 395,
                Description = "Basic membership" // Glöm inte Description om den är obligatorisk
            },
            new MembershipEntity
            {
                Id = 2,
                Title = "Gold", // Ändrat från Name till Title
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

        // SEED GYM CLASSES
        modelBuilder.Entity<GymClassEntity>().HasData(
      new GymClassEntity // <- Måste vara Entity-namnet här!
      {
          Id = 1,
          Name = "Yoga Flow",
          Trainer = "Maria",
          StartTime = DateTime.Parse("2026-05-01 08:00"),
          EndTime = DateTime.Parse("2026-05-01 09:00"),
          Intensity = "Low"
      },
 
            new GymClassEntity
            {
                Id = 2,
                Name = "Crossfit",
                Trainer = "Erik",
                StartTime = DateTime.Parse("2026-05-01 17:00"),
                EndTime = DateTime.Parse("2026-05-01 18:00"),
                Intensity = "High"
            }
        );
    }
}