using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wms.Web.wwwroot.data;

public class WmsDbContextFactory : IDesignTimeDbContextFactory<WmsDbContext>
{
    public WmsDbContext CreateDbContext(string[] args)
    {
        // đọc appsettings.json khi chạy tool scaffold/migration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<WmsDbContext>();
        var connStr = configuration.GetConnectionString("WmsDb");

        optionsBuilder.UseSqlServer(connStr);

        return new WmsDbContext(optionsBuilder.Options);
    }
}