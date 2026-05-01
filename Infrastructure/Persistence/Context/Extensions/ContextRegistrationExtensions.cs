using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Se till att DataContext hittas (lägg till dess namespace om det behövs)
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Context.Extensions;

public static class ContextRegistrationExtensions
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env)
    {
        services.AddDbContext<DataContext>(options =>
        {
            if (env.IsDevelopment())
            {
              
                options.UseInMemoryDatabase("GymPortalDevDb");
            }
            else
            {
                // PRODUKTION: En modellerad relationsdatabas (SQL Server)
                var connection = configuration.GetConnectionString("ProductionDatabase")
                    ?? throw new InvalidOperationException("Missing connection string");

                options.UseSqlServer(connection);
            }
        });

        return services;
    }
}