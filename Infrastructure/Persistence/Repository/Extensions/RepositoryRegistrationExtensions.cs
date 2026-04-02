using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistance.Repository.Extensions;
public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositorys(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        return services;
    }
}
