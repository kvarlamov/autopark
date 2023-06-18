using System;
using System.Text.Json.Serialization;

namespace Vehicle.Contract.Dto
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
    
    public class PlaceDto
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        
        
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string Cafe { get; set; }
        public string Road { get; set; }
        public string Suburb { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }
}