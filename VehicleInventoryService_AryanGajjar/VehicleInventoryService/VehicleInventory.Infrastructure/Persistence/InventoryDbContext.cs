using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var v = modelBuilder.Entity<Vehicle>();

        v.HasKey(x => x.Id);

        v.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        v.Property(x => x.VehicleCode)
            .IsRequired()
            .HasMaxLength(20);

        v.HasIndex(x => x.VehicleCode).IsUnique();

        v.Property(x => x.LocationId)
            .IsRequired()
            .HasMaxLength(50);

        v.Property(x => x.VehicleType)
            .IsRequired()
            .HasMaxLength(50);

        v.Property(x => x.Status)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}