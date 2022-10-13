using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Andromeda.Db;

public class AndromedaContextFactory : IDesignTimeDbContextFactory<AndromedaContext>
{
    public AndromedaContext CreateDbContext(string[] args)
    {
        var environment = args.FirstOrDefault();

        Console.WriteLine($"Creating AndromedaContext for \"{environment}\" environment.");
        
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<AndromedaContext>();
        
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Andromeda")!);

        return new AndromedaContext(optionsBuilder.Options);
    }
}