using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    /// <summary>
    /// Сервис транспортных средств
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly VehicleContext _db;

        public VehicleService(VehicleContext db)
        {
            _db = db;
        }
        
        public async Task<List<VehicleDto>> GetVehicles()
        {
            var vehicles = await _db.Vehicles.ToListAsync();
            
            //todo - add automapper
            List<VehicleDto> vehicleDtos = new List<VehicleDto>();

            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto()
                {
                    Color = vehicle.Color,
                    VehicleState = vehicle.VehicleState,
                    Cost = vehicle.Cost,
                    Mileage = vehicle.Mileage,
                    ManufactureYear = vehicle.ManufactureYear,
                    Transmission = vehicle.Transmission
                });
            }

            return vehicleDtos;
        }
    }
}