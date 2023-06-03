using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Enterprise.Contract.Dto;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Enterprise = AutoPark.Svc.Infrastructure.Entities.Enterprise;

namespace AutoPark.Svc
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly VehicleContext _db;

        public EnterpriseService(VehicleContext db)
        {
            _db = db;
        }
        
        public async Task<List<EnterpriseDto>> GetEnterprisesAsync()
        {
            var enterprises = await _db.Enterprises
                .Include(e => e.Drivers)
                .Include(e => e.Vehicles)
                .Include(e => e.Managers)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<EnterpriseDto>();

            foreach (var enterprise in enterprises)
            {
                result.Add(MapEnterpriseEntityToDto(enterprise));
            }

            return result;
        }

        public async Task<List<EnterpriseDto>> GetEnterprisesByIdsAsync(List<long> ids)
        {
            var enterprises = await _db.Enterprises
                .Include(e => e.Drivers)
                .Include(e => e.Vehicles)
                .Include(e => e.Managers)
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
            
            var result = new List<EnterpriseDto>();

            foreach (var enterprise in enterprises)
            {
                result.Add(MapEnterpriseEntityToDto(enterprise));
            }

            return result;
        }

        public async Task<EnterpriseDto> GetEnterpriseByIdAsync(long id)
        {
            var enterprise = await _db.Enterprises
                .Include(e => e.Drivers)
                .Include(e => e.Vehicles)
                .Include(e => e.Managers)
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return MapEnterpriseEntityToDto(enterprise);
        }

        public async Task<EnterpriseDto> GetEnterprise(long id)
        {
            var enterprise = await _db.Enterprises
                .Include(e => e.Drivers)
                .Include(e => e.Vehicles)
                .Include(e => e.Managers)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (enterprise == null)
                throw new Exception($"Entity with id {id} not found");

            return MapEnterpriseEntityToDto(enterprise);
        }

        public async Task<EnterpriseDto> UpdateAsync(EnterpriseDto dto)
        {
            throw new NotImplementedException();
        }

        private static Infrastructure.Entities.Enterprise MapDtoToEnterpriseEntity(EnterpriseDto dto)
        {
            throw new NotImplementedException();
            
            return new Infrastructure.Entities.Enterprise()
            {
                Name = dto.Name,
                Code = dto.Code,
                
            };
        }

        private static EnterpriseDto MapEnterpriseEntityToDto(Infrastructure.Entities.Enterprise enterprise)
        {
            List<long> drivers = new List<long>();
            if (enterprise.Drivers is {Count: > 0})
            {
                drivers.AddRange(enterprise.Drivers.Select(x => x.Id));
            }

            List<long> vehicles = new List<long>();
            if (enterprise.Vehicles is {Count: > 0})
            {
                vehicles.AddRange(enterprise.Vehicles.Select(x => x.Id));
            }

            List<long> managers = new List<long>();
            if (enterprise.Managers is {Count: > 0})
            {
                managers.AddRange(enterprise.Managers.Select(x => x.Id));
            }

            return new EnterpriseDto()
            {
                Id = enterprise.Id,
                City = enterprise.City,
                Name = enterprise.Name,
                Code = enterprise.Code,
                Drivers = drivers,
                NumberOfStaff = enterprise.NumberOfStaff,
                Vehicles = vehicles,
                Managers = managers
            };
        }
    }
}