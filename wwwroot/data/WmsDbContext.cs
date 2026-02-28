using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;

namespace Wms.Web.wwwroot.data;

public class WmsDbContext : DbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options) { }
    public DbSet<ContainerRecord> Containers => Set<ContainerRecord>();
    public DbSet<Sku> Skus => Set<Sku>();
    public DbSet<BinLocation> BinLocations => Set<BinLocation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContainerRecord>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.ContainerNo).HasMaxLength(15).IsRequired();
            e.HasIndex(x => x.ContainerNo).IsUnique();
            e.Property(x => x.Type).HasMaxLength(20).IsRequired();
            e.Property(x => x.Size).HasMaxLength(10).IsRequired();
            e.Property(x => x.Line).HasMaxLength(60).IsRequired();
            e.Property(x => x.CargoStatus).HasMaxLength(10).IsRequired();
            e.Property(x => x.CustomsStatus).HasMaxLength(60);
        });

        modelBuilder.Entity<Sku>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50).IsRequired();
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Unit).HasMaxLength(30);
            e.HasIndex(x => x.Code).IsUnique();
        });

        modelBuilder.Entity<BinLocation>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50).IsRequired();
            e.Property(x => x.Description).HasMaxLength(200);
            e.HasIndex(x => x.Code).IsUnique();
        });
    }
}