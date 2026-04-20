using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;



namespace Infrastructure.Persistence.Context.Extensions;
public static class ContextRegistrationExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        { 
            Console.WriteLine("Development environment detected. Using in-memory SQLite database.");
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
        else
        {
            Console.WriteLine("Production environment detected. Using SQL Server database.");
            services.AddDbContext<DataContext>((sp, options) =>
            {
                var connection = configuration.GetConnectionString("ProductionDatabase")
    ?? throw new ArgumentException("ProductionDatabase not provided");

                options.UseSqlServer(connection);
            });
        }
       
    
        return services;
    }
}
 