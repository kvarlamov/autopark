using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Contract.Dto;

namespace Vehicle.Contract
{
    public interface ITrackPointService
    {
        public Task<List<TrackPointDto>> GetTrackPointForVehicle(TrackRequestDto vehicleId);
        
        public Task<TrackPointDto> GetActualTrackPointForVehicle(long vehicleId);

        public Task<TrackPointDto> CreateTrackPoint(long vehicleId);
    }
}