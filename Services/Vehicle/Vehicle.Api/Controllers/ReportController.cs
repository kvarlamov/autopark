using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IVehicleReportForPeriodService _reportForPeriodService;

        public ReportController(IVehicleReportForPeriodService reportForPeriodService)
        {
            _reportForPeriodService = reportForPeriodService;
        }
        
        [HttpGet]
        public async Task<VehicleReportForPeriodResponseDto> GetReport([FromQuery]VehicleReportForPeriodRequestDto request) => 
            await _reportForPeriodService.GetReport(request);
    }
}