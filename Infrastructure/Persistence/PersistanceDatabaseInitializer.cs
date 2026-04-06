using Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistance
{
    public static class PersistanceDatabaseInitializer
    {
        public static async Task Initialize(IServiceProvider sp, IHostEnvironment env, CancellationToken ct = default)
        {
            if (env.IsDevelopment())
            {
                using var scope = sp.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                await context.Database.EnsureCreatedAsync(ct);
            }
            else
            {
                    using var scope = sp.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    await context.Database.MigrateAsync(ct);
            }
                    
            }
    }
}
