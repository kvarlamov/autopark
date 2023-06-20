using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Driver.Contract;
using Driver.Contract.Dto;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Driver = AutoPark.Svc.Infrastructure.Entities.Driver;

namespace AutoPark.Svc
{
    public class DriverService : IDriverService
    {
        private readonly VehicleContext _db;
        private readonly IVehicleService _vehicleService;

        public DriverService(VehicleContext db, IVehicleService vehicleService)
        {
            _db = db;
            _vehicleService = vehicleService;
        }
        
        public async Task<List<DriverDto>> GetDriversAsync()
        {
            var drivers = await _db.Drivers
                .Include(x => x.Vehicles)
                .AsNoTracking()
                .ToListAsync();
            
            //todo - add automapper
            var vehicleDtos = new List<DriverDto>();

            foreach (var driver in drivers)
            {
                vehicleDtos.Add(MapDriverEntityToDto(driver));
            }

            return vehicleDtos;
        }

        public async Task<DriverDto> CreateAsync(DriverDto driverDto)
        {
            var newEntity = MapDtoToDriverEntity(driverDto);

            if (driverDto.Vehicles is {Count: > 0})
            {
                var vehicles = await _db.Vehicles.Where(x => driverDto.Vehicles.Contains(x.Id)).ToListAsync();
                newEntity.Vehicles.AddRange(vehicles);
            }

            _db.Drivers.Add(newEntity);
            await _db.SaveChangesAsync();

            return MapDriverEntityToDto(newEntity);
        }

        public async Task<List<DriverDto>> GetFreeDriversAsync(long[] list)
        {
            var drivers = await _db.Drivers
                .Where(x => list.Contains(x.Id) && x.OnVehicleId == null)
                .AsNoTracking()
                .ToListAsync();
            
            // todo - add automapper
            var vehicleDtos = new List<DriverDto>();

            foreach (var driver in drivers)
            {
                vehicleDtos.Add(MapDriverEntityToDto(driver));
            }

            return vehicleDtos;
        }

        public async Task<DriverDto> GetDriverByIdAsync(long id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null)
                throw new Exception($"Driver with id {id} not found");

            return MapDriverEntityToDto(driver);
        }

        private static Infrastructure.Entities.Driver MapDtoToDriverEntity(DriverDto driverDto) =>
            new Infrastructure.Entities.Driver()
            {
                Name = driverDto.Name,
                Age = driverDto.Age,
                Salary = driverDto.Salary,
                EnterpriseId = driverDto.Enterprise,
                OnVehicleId = driverDto.OnVehicle
            };

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