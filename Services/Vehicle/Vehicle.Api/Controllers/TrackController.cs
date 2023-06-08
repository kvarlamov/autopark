using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase, ITrackPointService
    {
        private readonly ITrackPointService _trackPointService;

        public TrackController(ITrackPointService trackPointService)
        {
            _trackPointService = trackPointService;
        }
        
        [HttpGet("{vehicleId:long}")]
        public async Task<List<TrackPointDto>> GetTrackPointForVehicle(long vehicleId) => 
            await _trackPointService.GetTrackPointForVehicle(vehicleId);

        [HttpGet("last/{vehicleId:long}")]
        public async Task<TrackPointDto> GetActualTrackPointForVehicle(long vehicleId) =>
            await _trackPointService.GetActualTrackPointForVehicle(vehicleId);

        [HttpPost]
        public async Task<TrackPointDto> CreateTrackPoint(long vehicleId) => 
            await _trackPointService.CreateTrackPoint(vehicleId);
    }
}