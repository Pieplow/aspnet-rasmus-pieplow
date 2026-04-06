using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Application.Memberships;


namespace Application.Extensions;

public static class ApplicationServiceCollectionRegistrationExtentions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {

        services.AddScoped<IMembershipService, MembershipService>();

        return services;
    }
}
