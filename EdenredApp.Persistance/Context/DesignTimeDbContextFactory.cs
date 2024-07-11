using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EdenredApp.Persistance.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EdenredAppContext>
{
    public EdenredAppContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .Build();

        var builder = new DbContextOptionsBuilder<EdenredAppContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseNpgsql(connectionString);

        return new EdenredAppContext(builder.Options);
    }
}