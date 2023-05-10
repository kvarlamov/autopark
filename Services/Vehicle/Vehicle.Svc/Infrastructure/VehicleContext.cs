using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoPark.Svc.Infrastructure
{
    public class VehicleContext : DbContext
    {
        public DbSet<AutoPark.Svc.Infrastructure.Entities.Vehicle> Vehicles { get; set; }
        public DbSet<AutoPark.Svc.Infrastructure.Entities.Brand> Brands { get; set; }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoPark.Svc.Infrastructure.Entities.Brand>()
                .HasMany(b => b.Vehicles)
                .WithOne(v => v.Brand)
                .HasForeignKey(v => v.BrandId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}