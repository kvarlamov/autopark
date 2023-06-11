using System;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class TrackPoint
    {
        public long Id { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public DateTimeOffset TrackTime { get; set; }

        public long VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}