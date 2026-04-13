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

            await context.Database.MigrateAsync(ct);
        }
    }
}
