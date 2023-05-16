using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoPark.Svc.Infrastructure
{
    public class VehicleContext : DbContext
    {
        public DbSet<AutoPark.Svc.Infrastructure.Entities.Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<Entities.Driver> Drivers { get; set; }

        public DbSet<Entities.Enterprise> Enterprises { get; set; }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo - add Indexes for columns
            
            modelBuilder.Entity<AutoPark.Svc.Infrastructure.Entities.Brand>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Brand)
                .HasForeignKey(v => v.BrandId);

            modelBuilder.Entity<Entities.Vehicle>()
                .HasOne(v => v.ActiveDriver)
                .WithOne(d => d.OnVehicle)
                .HasForeignKey<Entities.Driver>(d => d.OnVehicleId);

            modelBuilder.Entity<Entities.Vehicle>()
                .HasMany(v => v.Drivers)
                .WithMany(d => d.Vehicles)
                .UsingEntity(j => j.ToTable("Vehicles_Drivers"));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}