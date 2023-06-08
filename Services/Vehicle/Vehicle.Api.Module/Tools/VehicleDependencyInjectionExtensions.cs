using AutoPark.Svc;
using AutoPark.Svc.Infrastructure;
using Driver.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vehicle.Contract;

namespace Vehicle.Api.Module.Tools
{
    public static class VehicleDependencyInjectionExtensions
    {
        public static void AddVehicleDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<VehicleContext>(options => options.UseNpgsql(config.GetConnectionString("AutoParkDB")));

            // Add Services
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITrackPointService, TrackPointService>();
        }
    }
}