using System.Collections.Generic;

namespace WebClient.Shared
{
    public class VehicleReportForPeriodResponseDto
    {
        public long VehicleId { get; set; }
        
        public string Name { get; set; }

        public ReportType ReportType { get; set; }
        
        public List<string> Result { get; set; }
    }
}