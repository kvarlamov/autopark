﻿using System.Collections.Generic;
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
        

        [HttpGet("GetTripPoints")]
        public async Task <List<TrackPointDto>> GetTripsTrackPointsAsync([FromQuery]TripRequestDto request) => 
            await _tripService.GetTripsTrackPointsAsync(request);
    }
}