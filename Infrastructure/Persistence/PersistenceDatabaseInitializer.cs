
using Infrastructure.Persistence.Context.Extensions;
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

        // Skriver ut miljön i terminalen vid start
        Console.WriteLine($"\n>>> SYSTEM-CHECK: Miljö = {env.EnvironmentName}");

        if (env.IsDevelopment())
        {
            Console.WriteLine(">>> DATABAS: Använder InMemory (Utvecklingsläge)");
            await context.Database.EnsureCreatedAsync(ct);
        }
        else
        {
            Console.WriteLine(">>> DATABAS: Använder SQL Server (Produktionsläge)");
            await context.Database.MigrateAsync(ct);
        }

        Console.WriteLine(">>> STATUS: Databasen är redo och initierad.\n");
    }
}