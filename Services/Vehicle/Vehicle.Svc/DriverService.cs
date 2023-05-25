﻿using System.Collections.Generic;
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
            var vehicles = await _vehicleService.GetVehiclesByIds(driverDto.Vehicles);
            var newEntity = MapDtoToDriverEntity(driverDto);
            //todo - need to provide correct vehicles
            newEntity.Vehicles = null;

            return MapDriverEntityToDto(newEntity);
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