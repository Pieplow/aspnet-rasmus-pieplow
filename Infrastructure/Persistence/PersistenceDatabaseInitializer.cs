using Infrastructure.Identity;
using Infrastructure.Persistence.Context.Extensions;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence;

public static class PersistenceDatabaseInitializer
{
    public static async Task Initialize(IServiceProvider sp, IHostEnvironment env, CancellationToken ct = default)
    {
        using var scope = sp.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        // Hämta Identity-tjänsterna från DI-containern
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        Console.WriteLine($"\n>>> SYSTEM-CHECK: Miljö = {env.EnvironmentName}");

        // 1. Först ser vi till att databasen existerar
        if (env.IsDevelopment())
        {
            Console.WriteLine(">>> DATABAS: Använder InMemory");
            await context.Database.EnsureCreatedAsync(ct);
        }
        else
        {
            Console.WriteLine(">>> DATABAS: Använder SQL Server");
            await context.Database.MigrateAsync(ct);
        }

        // 2. Sedan skapar vi rollerna (Viktigt för ditt VG!)
        string[] roles = { "Admin", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($">>> SEED: Roll '{role}' skapad.");
            }
        }

        
        var adminEmail = "admin@domain.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            // Lösenordet som läraren ska använda för admin
            var result = await userManager.CreateAsync(adminUser, "Admin123!?");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine($">>> SEED: Admin-konto skapat ({adminEmail})");
            }
        }

        Console.WriteLine(">>> STATUS: Databasen är redo, rollerna är på plats och admin är seedad.\n");
    }
}