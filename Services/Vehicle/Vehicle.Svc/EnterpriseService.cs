using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Enterprise.Contract.Dto;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;

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
            var enterprises = await _db.Enterprises.ToListAsync();

            var result = new List<EnterpriseDto>();

            foreach (var enterprise in enterprises)
            {
                result.Add(MapEnterpriseEntityToDto(enterprise));
            }

            return result;
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

            return new EnterpriseDto()
            {
                City = enterprise.City,
                Name = enterprise.Name,
                Code = enterprise.Code,
                Drivers = drivers,
                NumberOfStaff = enterprise.NumberOfStaff,
                Vehicles = vehicles
            };
        }
    }
}