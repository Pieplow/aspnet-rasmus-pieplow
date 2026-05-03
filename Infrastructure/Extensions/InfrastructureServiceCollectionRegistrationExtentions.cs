using Application.Bookings;
using Infrastructure.Identity;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Repositories.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;




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

    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<Application.Account.IIdentityService, Infrastructure.Services.IdentityService>();
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()                
        .AddEntityFrameworkStores<DataContext>() 
        .AddDefaultTokenProviders();

        return services;
    }
}
