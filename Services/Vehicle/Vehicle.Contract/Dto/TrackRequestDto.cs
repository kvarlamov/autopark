using System;

namespace Vehicle.Contract.Dto
{
    public class TrackRequestDto
    {
        public long VehicleId { get; set; }

        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }

        public bool UseGeoJson { get; set; }
    }
}