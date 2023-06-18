using System;

namespace Vehicle.Contract.Dto
{
    public record TripRequestDto(long VehicleId, DateTimeOffset? StartTime, DateTimeOffset? EndTime, long? tripId= null);
}