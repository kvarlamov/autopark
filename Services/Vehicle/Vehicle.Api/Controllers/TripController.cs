using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    public class TripController : ControllerBase, ITripService
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet("GetTrips")]
        public async Task<List<TripDto>> GetTrips([FromQuery]TripRequestDto request) =>
            await _tripService.GetTrips(request);

        [HttpGet("GetTripById")]
        public async Task<List<TrackPointDto>> GetTripPoints([FromQuery] TripRequestDto request) => 
            await _tripService.GetTripPoints(request);


        [HttpGet("GetTripPoints")]
        public async Task <List<TrackPointDto>> GetTripsTrackPointsAsync([FromQuery]TripRequestDto request) => 
            await _tripService.GetTripsTrackPointsAsync(request);

        public Task AddTrackPointToTrip(TrackPointDto newPoint, decimal avSpeed, decimal distance)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TrackPointDto>> GetTripPointsForReport(TripRequestDto request)
        {
            throw new System.NotImplementedException();
        }
    }
}