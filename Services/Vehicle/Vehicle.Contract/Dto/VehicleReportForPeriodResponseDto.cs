using System.Collections.Generic;
using Vehicle.Contract.Enums;

namespace Vehicle.Contract.Dto
{
    public class VehicleReportForPeriodResponseDto
    {
        public long VehicleId { get; set; }

        public ReportType ReportType { get; set; }
        
        public List<string> Result { get; set; }
    }
}