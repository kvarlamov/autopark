using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Contract.Dto;

namespace Vehicle.Contract
{
    public interface ITripService
    {
        public Task<List<TripDto>> GetTrips(TripRequestDto request);

        public Task<List<TrackPointDto>> GetTripPoints(TripRequestDto request);

        public Task<List<TrackPointDto>> GetTripsTrackPointsAsync(TripRequestDto request);
    }
}