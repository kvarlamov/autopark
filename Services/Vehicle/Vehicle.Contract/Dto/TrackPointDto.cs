using System;

namespace Vehicle.Contract.Dto
{
    public class TrackPointDto
    {
        public long Id { get; set; }

        public DateTimeOffset TrackTime { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public long VehicleId { get; set; }
    }
}