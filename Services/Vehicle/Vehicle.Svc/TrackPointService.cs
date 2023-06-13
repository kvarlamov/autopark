using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    public class TrackPointService : ITrackPointService
    {
        private readonly VehicleContext _db;

        public TrackPointService(VehicleContext db)
        {
            _db = db;
        }
        
        public async Task<List<TrackPointDto>> GetTrackPointForVehicle(TrackPointRequestDto pointRequest)
        {
            //todo - need to add to service returning of offset
            var vehicle = await _db.Vehicles
                .AsNoTracking()
                .Include(x => x.Enterprise)
                .FirstAsync(x => x.Id == pointRequest.VehicleId);

            var enterpriseOffset = vehicle.Enterprise.TimezoneOffset ?? 0;

            var resultEntities = _db.TrackPoints
                .AsNoTracking()
                .Where(x => x.VehicleId == pointRequest.VehicleId);

            if (pointRequest.From != null)
            {
                pointRequest.From = pointRequest.From.Value.AddHours(-enterpriseOffset);
                resultEntities = resultEntities.Where(x => x.TrackTime >= pointRequest.From.Value);
            }

            if (pointRequest.To != null)
            {
                pointRequest.To = pointRequest.To.Value.AddHours(-enterpriseOffset);
                resultEntities = resultEntities.Where(x => x.TrackTime <=pointRequest.To.Value);
            }

            var result = await resultEntities.Include(x => x.Vehicle)
                .ThenInclude(v => v.Enterprise)
                .ToArrayAsync();

            var list = new List<TrackPointDto>();
            foreach (var entity in result)
                list.Add(MapEntityToDto(entity));

            return list;
        }

        public async Task<TrackPointDto> GetActualTrackPointForVehicle(long vehicleId)
        {
            var result = await _db.TrackPoints
                .AsNoTracking()
                .Where(x => x.VehicleId == vehicleId)
                .OrderByDescending(x => x.TrackTime)
                .Include(x => x.Vehicle)
                .ThenInclude(v => v.Enterprise)
                .FirstOrDefaultAsync();

            return MapEntityToDto(result);
        }

        public async Task<TrackPointDto> CreateTrackPoint(long vehicleId)
        {
            var vehicle = await _db.Vehicles
                .AsNoTracking()
                .Include(x => x.Enterprise)
                .FirstOrDefaultAsync(x => x.Id == vehicleId);
            
            var newTrackPoint = new TrackPoint
            {
                VehicleId = vehicle.Id,
                TrackTime = DateTimeOffset.Now
            };

            newTrackPoint.Latitude = GetRandomLatitude().ToString();
            newTrackPoint.Longitude = GetRandomLongitude().ToString();
            
            _db.TrackPoints.Add(newTrackPoint);

            await _db.SaveChangesAsync();

            newTrackPoint.Vehicle = vehicle;

            return MapEntityToDto(newTrackPoint);
        }

        public static TrackPointDto MapEntityToDto(TrackPoint entity)
        {
            var trackTime = entity.Vehicle.Enterprise.TimezoneOffset != null
                ? entity.TrackTime.AddHours(entity.Vehicle.Enterprise.TimezoneOffset.Value)
                : entity.TrackTime;
            
            return new TrackPointDto()
            {
                Id = entity.Id,
                TrackTime = trackTime,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                VehicleId = entity.VehicleId
            };
        }
        
        private static readonly Random random = new Random();

        public static double GetRandomLongitude(double minLongitude = -180, double maxLongitude = 180)
        {
            return random.NextDouble() * (maxLongitude - minLongitude) + minLongitude;
        }

        public static double GetRandomLatitude(double minLatitude = -90, double maxLatitude = 90)
        {
            return random.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
        }
    }
}