using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence.Repositories.Extensions;



namespace Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
      {
        services.AddPersistance(configuration, env);

        services.AddIdentityInfrastructure();
        return services;
      }
 }
