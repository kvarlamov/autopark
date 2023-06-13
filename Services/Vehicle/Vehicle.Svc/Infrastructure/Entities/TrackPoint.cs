using System;
using BaseTypes;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class TrackPoint : BaseEntity
    {
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public DateTimeOffset TrackTime { get; set; }

        public long VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}