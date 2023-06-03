using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Driver.Contract;
using Driver.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly ILogger<DriverController> _logger;

        public DriverController(
            IDriverService driverService,
            ILogger<DriverController> logger
            )
        {
            _driverService = driverService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<List<DriverDto>> GetDriversAsync()
        {
            return await _driverService.GetDriversAsync();
        }

        [HttpGet("{id:long}")]
        public async Task<DriverDto> GetDriverByIdAsync(long id)
        {
            return await _driverService.GetDriverByIdAsync(id);
        }

        [HttpGet("freelist")]
        public async Task<List<DriverDto>> GetFreeDriversAsync([FromQuery]string ids)
        {
            var list = ids.Split(",").Select(long.Parse).ToArray();
            return await _driverService.GetFreeDriversAsync(list);
        }
    }
}