using Application.Account;
using Infrastructure.Identity;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class IdentityRegistrationExtensions
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        
        services.AddScoped<IIdentityService, IdentityService>();

        // 2. Konfigurera Identity-motorn
        services.AddIdentityCore<ApplicationUser>(options =>
        {
           
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<DataContext>() 
        .AddDefaultTokenProviders()
        .AddSignInManager();

        return services;
    }
}