using Microsoft.EntityFrameworkCore;

namespace AutoPark.Svc.Infrastructure
{
    public class VehicleContext : DbContext
    {
        public DbSet<AutoPark.Svc.Infrastructure.Entities.Vehicle> Vehicles { get; set; }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }
    }
}