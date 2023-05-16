using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Driver.Contract;
using Driver.Contract.Dto;
using Microsoft.EntityFrameworkCore;

namespace AutoPark.Svc
{
    public class DriverService : IDriverService
    {
        private readonly VehicleContext _db;

        public DriverService(VehicleContext db)
        {
            _db = db;
        }
        
        public async Task<List<DriverDto>> GetDriversAsync()
        {
            var drivers = await _db.Drivers
                .Include(x => x.Vehicles)
                .AsNoTracking()
                .ToListAsync();
            
            //todo - add automapper
            List<DriverDto> vehicleDtos = new List<DriverDto>();

            foreach (var driver in drivers)
            {
                vehicleDtos.Add(MapDriverEntityToDto(driver));
            }

            return vehicleDtos;
        }

        private static DriverDto MapDriverEntityToDto(Infrastructure.Entities.Driver driver)
        {
            List<long> vehicles = new List<long>();
            if (driver.Vehicles is {Count: > 0})
                vehicles.AddRange(driver.Vehicles.Select(x => x.Id));

            return new DriverDto()
            {
                Id = driver.Id,
                Name = driver.Name,
                Age = driver.Age,
                Salary = driver.Salary,
                Enterprise = driver.EnterpriseId,
                Vehicles = vehicles,
                OnVehicle = driver.OnVehicleId
            };
        }
    }
}