using Infrastructure.Persistence.Context.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Infrastructure.Persistence.Repositories.Extensions;

public static class PersistanceRegistrationExtensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddDbContext(configuration, env);
        services.AddRepositorys(configuration, env);

        return services;
    }
}
