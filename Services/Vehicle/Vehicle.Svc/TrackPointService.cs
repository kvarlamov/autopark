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
        
        public async Task<List<TrackPointDto>> GetTrackPointForVehicle(long vehicleId)
        {
            var resultEntities = await _db.TrackPoints.AsNoTracking().Where(x => x.VehicleId == vehicleId).ToArrayAsync();

            var result = new List<TrackPointDto>();
            foreach (var entity in resultEntities)
            {
                result.Add(MapEntityToDto(entity));
            }

            return result;
        }

        public async Task<TrackPointDto> GetActualTrackPointForVehicle(long vehicleId)
        {
            var result = await _db.TrackPointLast.FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

            return MapEntityToDto(result);
        }

        public async Task<TrackPointDto> CreateTrackPoint(long vehicleId)
        {
            var newTrackPoint = new TrackPoint
            {
                VehicleId = vehicleId,
                TrackTime = DateTimeOffset.UtcNow
            };

            var existedLast = await _db.TrackPointLast.SingleOrDefaultAsync(x => x.VehicleId == vehicleId);
            if (existedLast != null)
            {
                existedLast.TrackTime = newTrackPoint.TrackTime;
            }
            else
            {
                _db.TrackPointLast.Add(new TrackPointLast()
                {
                    VehicleId = vehicleId,
                    TrackTime = newTrackPoint.TrackTime
                });
            }

            _db.TrackPoints.Add(newTrackPoint);

            await _db.SaveChangesAsync();

            return MapEntityToDto(newTrackPoint);
        }

        private TrackPointDto MapEntityToDto(TrackPoint entity)
        {
            return new TrackPointDto()
            {
                Id = entity.Id,
                TrackTime = entity.TrackTime,
                VehicleId = entity.VehicleId
            };
        }
        
        private TrackPointDto MapEntityToDto(TrackPointLast entity)
        {
            return new TrackPointDto()
            {
                Id = entity.Id,
                TrackTime = entity.TrackTime,
                VehicleId = entity.VehicleId
            };
        }
    }
}