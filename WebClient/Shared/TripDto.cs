using System;

namespace WebClient.Shared
{
    public class TripDto
    {
        public long Id { get; set; }
        
        public long VehicleId { get; set; }

        public PointInfo StartPlace { get; set; }

        public PointInfo? EndPlace { get; set; }
    }

    public class PointInfo
    {
        public string DisplayName { get; set; }
        
        public DateTimeOffset Time { get; set; }
        
    }

    public class TrackPointDto
    {
        public long Id { get; set; }

        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
    }
}