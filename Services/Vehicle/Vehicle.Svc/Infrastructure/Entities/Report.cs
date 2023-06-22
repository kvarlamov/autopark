using System;
using System.Collections.Generic;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class Report
    {
        public string Name { get; set; }

        public Interval Interval { get; set; }

        public DateTimeOffset StartDate { get; set; }
        
        public DateTimeOffset EndDate { get; set; }
    }

    public class VehicleReportForPeriod
    {
        public List<string> Result { get; set; }

        public ReportType ReportType => ReportType.VehicleForPeriodReport;
    }
}