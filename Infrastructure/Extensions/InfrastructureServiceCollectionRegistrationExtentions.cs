using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
      {
      return services;
      }
 }
