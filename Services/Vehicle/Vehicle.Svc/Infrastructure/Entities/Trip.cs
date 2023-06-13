using System;
using BaseTypes;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public sealed class Trip : BaseEntity
    {
        public long VehicleId { get; set; }
        
        public Vehicle Vehicle { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}