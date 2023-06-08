using System;

namespace Vehicle.Contract.Dto
{
    public class TrackPointDto
    {
        public long Id { get; set; }

        public DateTimeOffset TrackTime { get; set; }

        public long VehicleId { get; set; }
    }
}