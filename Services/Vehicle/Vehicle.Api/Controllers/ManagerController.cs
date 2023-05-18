using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly UserManager<Manager> _userManager;
        private readonly IManagerService _managerService;

        public ManagerController(UserManager<Manager> userManager, IManagerService managerService)
        {
            _userManager = userManager;
            _managerService = managerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _managerService.GetManagersAsync());
        }
    }
}