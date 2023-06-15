using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    public class TripService : ITripService
    {
        private readonly VehicleContext _db;
        private readonly HttpClient _client;

        public TripService(VehicleContext db, IHttpClientFactory clientFactory)
        {
            _db = db;
            _client = clientFactory.CreateClient();
        }

        public async Task<List<TripDto>> GetTrips(TripRequestDto request)
        {
            var (vehicleId, startTime, endTime) = request;

            var query = _db.Trips
                .AsNoTracking()
                .Include(x => x.Points)
                .Where(x => x.VehicleId == vehicleId 
                            && x.StartTime >= startTime 
                            && (x.EndTime <= endTime || x.EndTime == null));

            var trips = await query.ToListAsync();

            var result = new List<TripDto>();
            foreach (var trip in trips)
            {
                var tripDto = MapTripEntityToDto(trip);
                var startPoint = trip.Points.FirstOrDefault(x => x.TrackTime == trip.StartTime);
                var endPoint = trip.EndTime != null 
                    ? trip.Points.FirstOrDefault(x => x.TrackTime == trip.EndTime) 
                    : null;

                var responseStart = await _client.GetAsync(GetQueryString(startPoint!.Latitude, startPoint.Longitude));
                if (responseStart.IsSuccessStatusCode)
                {
                    var json = await responseStart.Content.ReadAsStringAsync();
                    var startPointPlace = JsonSerializer.Deserialize<PlaceDto>(json);
                    tripDto.StartPlace = new PointInfo
                    {
                        Time = startPoint.TrackTime,
                        DisplayName = startPointPlace.DisplayName
                    };
                    Thread.Sleep(10);
                }
                
                if (endPoint != null)
                {
                    var responseEnd = await _client.GetAsync(GetQueryString(endPoint!.Latitude, endPoint.Longitude));
                    if (responseEnd.IsSuccessStatusCode)
                    {
                        var json = await responseEnd.Content.ReadAsStringAsync();
                        var endPointPlace = JsonSerializer.Deserialize<PlaceDto>(json);
                        tripDto.EndPlace = new PointInfo
                        {
                            Time = endPoint.TrackTime,
                            DisplayName = endPointPlace.DisplayName
                        };
                        Thread.Sleep(10);
                    }
                }
                
                result.Add(tripDto);
            }

            return result;
        }

        public async Task<List<TrackPointDto>> GetTripsTrackPointsAsync(TripRequestDto request)
        {
            var (vehicleId, startTime, endTime) = request;
            startTime ??= DateTimeOffset.MinValue;
            endTime ??= DateTimeOffset.MaxValue;

            var tripsTrackPoints = await _db.Trips
                .AsNoTracking()    
                .Where(x => x.VehicleId == vehicleId && (x.StartTime >= startTime && x.EndTime <= endTime))
                .Include(x => x.Points)
                .SelectMany(x => x.Points)
                .Distinct()
                .OrderBy(x => x.TrackTime)
                .Where(x => x.TrackTime >= startTime && x.TrackTime <= endTime)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.Enterprise)
                .ToListAsync();

            var trackPoints = new List<TrackPointDto>();
            foreach (var trackPoint in tripsTrackPoints)
            {
                trackPoints.Add(TrackPointService.MapEntityToDto(trackPoint));
            }

            return trackPoints;
        }

        public TripDto MapTripEntityToDto(Trip trip) =>
            new TripDto()
            {
                VehicleId = trip.VehicleId
            };

        private string GetQueryString(string lat, string lon) =>
            $"https://us1.locationiq.com/v1/reverse?key=pk.a1a80f41933e77032ab4cfc1ee4a05dd&lat={lat}&lon={lon}&format=json";
    }
}