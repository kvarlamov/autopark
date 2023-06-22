using System;
using Vehicle.Contract.Enums;

namespace Vehicle.Contract.Dto
{
    public class VehicleReportForPeriodRequestDto
    {
        public long VehicleId { get; set; }

        public ReportType ReportType { get; set; }
        
        public Interval Interval { get; set; }
        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}