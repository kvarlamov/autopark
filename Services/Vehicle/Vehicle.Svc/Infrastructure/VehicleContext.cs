using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoPark.Svc.Infrastructure
{
    public class VehicleContext : IdentityDbContext<Manager, IdentityRole<long>, long>
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory();
        
        public DbSet<AutoPark.Svc.Infrastructure.Entities.Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<Entities.Driver> Drivers { get; set; }

        public DbSet<Entities.Enterprise> Enterprises { get; set; }
        // public DbSet<ManagerEnterprises> ManagerEnterprises { get; set; }

        public DbSet<TrackPoint> TrackPoints { get; set; }

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
            
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Enterprises)
                .WithMany(e => e.Managers)
                .UsingEntity(j => j.ToTable("ManagerEnterprises"));

            modelBuilder.Entity<TrackPoint>()
                .HasOne(e => e.Vehicle)
                .WithMany(v => v.TrackPoints)
                .HasForeignKey(v => v.VehicleId);
            
            //todo - уточнить нужно ли добавлять индекс если у нас внешний ключ
            
            base.OnModelCreating(modelBuilder);
        }
    }
}