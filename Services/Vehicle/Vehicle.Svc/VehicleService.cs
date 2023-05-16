using System;
using System.Collections.Generic;
using System.Linq;
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
            var vehicles = await _db.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.ActiveDriver)
                // .Include(v => v.Enterprise) // dont need to include because have long property
                .Include(v => v.Drivers)
                .AsNoTracking()
                .ToListAsync();
            
            //todo - add automapper
            List<VehicleDto> vehicleDtos = new List<VehicleDto>();

            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(MapVehicleEntityToDto(vehicle));
            }

            return vehicleDtos;
        }

        public async Task<VehicleDto> GetVehicle(long id)
        {
            //todo - add everywhere compiledquery
            var entity = await _db.Vehicles.Include(v => v.Brand).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            if (entity == null)
                throw new Exception($"Vehicle with id {id} not found");

            return MapVehicleEntityToDto(entity);
        }

        public async Task<VehicleDto> CreateAsync(VehicleDto dto)
        {
            //todo  - при создании продумать обработку бренда - создавать новый или выбирать из имеющихся
            var newEntity = MapVehicleDtoToEntity(dto);

            if (dto.BrandId == 0)
                throw new ArgumentException("Brand id cant be 0");
            
            var brand = await _db.Brands.FirstOrDefaultAsync(brand => brand.Id == dto.BrandId);
            if (brand != null)
                newEntity.Brand = brand;

            _db.Vehicles.Add(newEntity);
            await _db.SaveChangesAsync();

            return MapVehicleEntityToDto(newEntity);
        }

        public async Task<VehicleDto> UpdateAsync(VehicleDto dto)
        {
            var existed = await _db.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == dto.Id);
            if (existed is null)
                throw new ArgumentException($"Vehicle with id {dto.Id} not found");
            
            var newEntity = MapVehicleDtoToEntity(dto);
            var brand = await _db.Brands.FirstOrDefaultAsync(brand => brand.Id == dto.BrandId);
            if (brand != null)
                newEntity.Brand = brand;

            UpdateEntityWithNewFields(existed, newEntity);

            await _db.SaveChangesAsync();

            return MapVehicleEntityToDto(newEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existed = await _db.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (existed is null)
                throw new ArgumentException($"Vehicle with id {id} not found");

            _db.Vehicles.Remove(existed);
            await _db.SaveChangesAsync();

            return true;
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
        
        private static VehicleDto MapVehicleEntityToDto(Infrastructure.Entities.Vehicle vehicle)
        {
            List<long> drivers = new List<long>();
            if (vehicle.Drivers is {Count: > 0})
                drivers.AddRange(vehicle.Drivers.Select(x => x.Id));

            return new VehicleDto
            {
                Id = vehicle.Id,
                Color = vehicle.Color,
                VehicleState = vehicle.VehicleState,
                Cost = vehicle.Cost,
                Mileage = vehicle.Mileage,
                ManufactureYear = vehicle.ManufactureYear,
                Transmission = vehicle.Transmission,
                BrandName = vehicle.Brand.Name,
                BrandId = vehicle.Brand.Id,
                ActiveDriver = vehicle.ActiveDriver?.Id,
                Drivers = drivers,
                Enterprise = vehicle.EnterpriseId
            };
        }

        private Infrastructure.Entities.Vehicle MapVehicleDtoToEntity(VehicleDto dto) =>
            new()
            {
                Color = dto.Color,
                VehicleState = dto.VehicleState,
                Cost = dto.Cost,
                Mileage = dto.Mileage,
                ManufactureYear = dto.ManufactureYear,
                Transmission = dto.Transmission
            };
        
        private void UpdateEntityWithNewFields(Infrastructure.Entities.Vehicle existed, Infrastructure.Entities.Vehicle updated)
        {
            existed.Color = updated.Color;
            existed.VehicleState = updated.VehicleState;
            existed.Cost = updated.Cost;
            existed.Mileage = updated.Mileage;
            existed.ManufactureYear = updated.ManufactureYear;
            existed.Transmission = updated.Transmission;
            if (updated.Brand is not null)
                existed.Brand = updated.Brand;
        }
    }
}