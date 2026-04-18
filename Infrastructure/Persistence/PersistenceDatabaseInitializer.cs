using Infrastructure.Persistence.Context; // Se till att denna pekar på din DataContext
using Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    public static class PersistenceDatabaseInitializer
    {
        public static async Task Initialize(IServiceProvider sp, IHostEnvironment env, CancellationToken ct = default)
        {
            using var scope = sp.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (env.IsDevelopment())
            {
                // För SQLite In-Memory (VG-krav):
                // Skapar tabellerna direkt i RAM-minnet utan att läsa migrations-filer.
                // Detta gör att vi slipper syntax-felet "near max".
                await context.Database.EnsureCreatedAsync(ct);
            }
            else
            {
                // För SQL Server (När du kör i Production-läge för Postman/Swagger):
                // Kör de riktiga migreringarna mot SQL Server Management Studio.
                await context.Database.MigrateAsync(ct);
            }
        }
    }
}