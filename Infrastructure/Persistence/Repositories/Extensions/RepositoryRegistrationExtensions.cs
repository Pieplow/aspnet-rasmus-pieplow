using Domain.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Repositories.Extensions;
public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositorys(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        return services;
    }
}
