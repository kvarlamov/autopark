using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    public class TripService : ITripService
    {
        private readonly VehicleContext _db;

        public TripService(VehicleContext db)
        {
            _db = db;
        }
        
        public async Task<List<TrackPointDto>> GetTripsTrackPointsAsync(TripRequestDto request)
        {
            var (vehicleId, startTime, endTime) = request;
            if (startTime == null)
                startTime = DateTimeOffset.MinValue;
            
            if (endTime == null)
                endTime = DateTimeOffset.MaxValue;

            var tripsTrackPoints = await _db.Trips
                .AsNoTracking()    
                .Where(x => x.VehicleId == vehicleId && (x.StartTime >= startTime && x.EndTime <= endTime))
                .Include(x => x.Vehicle)
                .ThenInclude(v => v.TrackPoints)
                .SelectMany(x => x.Vehicle.TrackPoints)
                .Distinct()
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
    }
}