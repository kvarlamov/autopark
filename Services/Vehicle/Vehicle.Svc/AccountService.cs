using System;
using System.Threading.Tasks;
using AutoPark.Api.Models;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Vehicle.Contract;

namespace AutoPark.Svc
{
    public class AccountService : IAccountService
    {
        private readonly VehicleContext _db;
        private readonly SignInManager<Manager> _signInManager;
        private readonly UserManager<Manager> _userManager;

        public AccountService(VehicleContext db, SignInManager<Manager> signInManager, UserManager<Manager> userManager)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public async Task<LoginViewModel> CheckLogin(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded) return null;
            
            var manager = await _userManager.FindByEmailAsync(model.UserName);
            if (manager == null)
                return null;
            
            var userRoles = await _userManager.GetRolesAsync(manager);
            model.Roles.AddRange(userRoles);
            return model;
        }
    }
}