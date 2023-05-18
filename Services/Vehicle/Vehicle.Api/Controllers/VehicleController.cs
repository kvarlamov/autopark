using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(
            IVehicleService vehicleService,
            ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<VehicleDto>> Get()
        {
            return await _vehicleService.GetVehicles();
        }
        
        [HttpGet("{id:long}")]
        public async Task<VehicleDto> Get(long id)
        {
            return await _vehicleService.GetVehicle(id);
        }

        [HttpPost]
        public async Task<VehicleDto> CreateAsync([FromBody] VehicleDto dto)
        {
            var res = await _vehicleService.CreateAsync(dto);

            return res;
        }
        
        [HttpPut]
        public async Task<VehicleDto> UpdateAsync([FromBody] VehicleDto dto)
        {
            var res = await _vehicleService.UpdateAsync(dto);

            return res;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            var res = await _vehicleService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("brand")]
        public async Task<List<BrandDto>> GetBrands()
        {
            return await _vehicleService.GetBrands();
        }
    }
}