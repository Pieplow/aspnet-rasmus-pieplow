using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence.Repositories.Extensions;
using Application.Bookings;



namespace Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
      {
        services.AddPersistance(configuration, env);

        
        services.AddScoped<Application.GymClasses.IGymClassService, Application.GymClasses.GymClassService>();

        
        services.AddScoped<IBookingService, BookingService>();

        services.AddIdentityInfrastructure();
        return services;
      }
 }
