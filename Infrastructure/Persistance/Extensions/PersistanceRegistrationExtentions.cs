using Infrastructure.Persistance.Context.Extensions;
using Infrastructure.Persistance.Repository.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistance.Extensions;

public static class PersistanceRegistrationExtensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddDbContext(configuration, env);
        services.AddRepositorys(configuration, env);

        return services;
    }
}
