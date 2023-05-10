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
            var vehicles = await _db.Vehicles.Include(v => v.Brand)
                .AsNoTracking()
                .ToListAsync();
            
            //todo - add automapper
            List<VehicleDto> vehicleDtos = new List<VehicleDto>();

            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto()
                {
                    Id = vehicle.Id,
                    Color = vehicle.Color,
                    VehicleState = vehicle.VehicleState,
                    Cost = vehicle.Cost,
                    Mileage = vehicle.Mileage,
                    ManufactureYear = vehicle.ManufactureYear,
                    Transmission = vehicle.Transmission,
                    BrandName = vehicle.Brand.Name,
                    BrandId = vehicle.Brand.Id
                });
            }

            return vehicleDtos;
        }

        public async Task<List<BrandDto>> GetBrands()
        {
            var brands = await _db.Brands.AsNoTracking().ToListAsync();
            
            //todo - add automapper
            List<BrandDto> brandDtos = new List<BrandDto>();

            foreach (var brand in brands)
            {
                brandDtos.Add(new BrandDto()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Tank = brand.Tank,
                    LoadCapacity = brand.LoadCapacity,
                    VehicleType = brand.VehicleType,
                    NumberOfSeats = brand.NumberOfSeats
                });
            }

            return brandDtos;
        }
    }
}