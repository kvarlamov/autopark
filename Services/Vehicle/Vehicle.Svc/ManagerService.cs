using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    public class ManagerService : IManagerService
    {
        private readonly VehicleContext _db;
        private readonly UserManager<Manager> _userManager;

        public ManagerService(VehicleContext db, UserManager<Manager> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public async Task<List<ManagerDto>> GetManagersAsync()
        {
            List<ManagerDto> managerDtos = new(); 

            var managers = await _userManager.Users.ToListAsync();
            foreach (var manager in managers)
            {
                var enterprises = await _db.Entry(manager)
                    .Collection(m => m.Enterprises)
                    .Query()
                    .Select(x => x.Id)
                    .ToListAsync();
                
                managerDtos.Add(new ManagerDto()
                {
                    Id = manager.Id,
                    Email = manager.Email,
                    UserName = manager.UserName,
                    Enterprises = enterprises
                });
            }

            return managerDtos;
        }
    }
}