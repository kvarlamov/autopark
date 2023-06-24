using System.Collections.Generic;

namespace WebClient.Shared
{
    public class ReportVehicleDto
    {
        public long VehicleId { get; set; }

        public string Name { get; set; }

        public ReportType ReportType { get; set; }
        
        public List<string> Result { get; set; }
    }
    
    public enum Interval
    {
        Day = 1,
        Month = 2,
        Year = 3
    }

    public enum ReportType
    {
        VehicleForPeriodReport = 1
    }
}