using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure.Entities;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackPointService _trackPointService;

        public TrackController(ITrackPointService trackPointService)
        {
            _trackPointService = trackPointService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTrackPointForVehicle([FromQuery]TrackRequestDto request)
        {
            var result = await _trackPointService.GetTrackPointForVehicle(request);

            if (request.UseGeoJson)
            {
                return MapResultToGeoJson(result);
            }

            return new JsonResult(result);
        }


        [HttpGet("last/{vehicleId:long}")]
        public async Task<TrackPointDto> GetActualTrackPointForVehicle(long vehicleId) =>
            await _trackPointService.GetActualTrackPointForVehicle(vehicleId);

        [HttpPost]
        public async Task<TrackPointDto> CreateTrackPoint(long vehicleId) => 
            await _trackPointService.CreateTrackPoint(vehicleId);
        
        private ContentResult MapResultToGeoJson(List<TrackPointDto> dtos)
        {
            var geoJsonFeatures = new List<object>();

            foreach (var trackPoint in dtos)
            {
                var geoJsonFeature = new
                {
                    type = "FeatureCollection",
                    features = new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { trackPoint.Longitude, trackPoint.Latitude } // Assuming Longitude and Latitude properties for coordinates
                        },
                        properties = new
                        {
                            // Include any additional properties you want in the GeoJSON features
                        }
                    }
                };
                geoJsonFeatures.Add(geoJsonFeature);
            }
            
            string geoJson = JsonConvert.SerializeObject(geoJsonFeatures);

            return new ContentResult
            {
                Content = geoJson,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}