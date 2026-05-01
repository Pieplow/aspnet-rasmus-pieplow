using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence.Context.Extensions; 
namespace Infrastructure.Persistence.Context;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
    
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Presentation.WebApp");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        // Hämta din produktions-sträng
        var connectionString = configuration.GetConnectionString("ProductionDatabase");

        
        optionsBuilder.UseSqlServer(connectionString);

        return new DataContext(optionsBuilder.Options);
    }
}