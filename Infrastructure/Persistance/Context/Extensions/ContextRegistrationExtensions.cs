using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;


namespace Infrastructure.Persistance.Context.Extensions;
public static class ContextRegistrationExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {

            services.AddSingleton<SqliteConnection>(_ =>
            {
                var connection = new SqliteConnection("Data Source=:memory:;");
                connection.Open();
                return connection;
            });

            services.AddDbContext<DataContext>((sp, options) =>
                {
                    var connection = sp.GetRequiredService<SqliteConnection>();
                    options.UseSqlite(connection);
                });
        
        }
       
    
        return services;
    }
}
 