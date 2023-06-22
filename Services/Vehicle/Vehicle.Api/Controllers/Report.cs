using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    public class ReportController : ControllerBase
    {
        private readonly IVehicleReportForPeriodService _reportForPeriodService;

        public ReportController(IVehicleReportForPeriodService reportForPeriodService)
        {
            _reportForPeriodService = reportForPeriodService;
        }
        
        [HttpGet]
        public Task<VehicleReportForPeriodResponseDto> GetReport([FromQuery]VehicleReportForPeriodRequestDto request) => 
            _reportForPeriodService.GetReport(request);
    }
}