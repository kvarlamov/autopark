using System.Collections.Generic;
using BaseTypes;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class Driver : BaseEntity
    {
        /// <summary>
        /// First Name + Last Name
        /// </summary>
        public string Name { get; set; }

        public int Age { get; set; }

        public decimal Salary { get; set; }

        public long EnterpriseId { get; set; }

        public Enterprise Enterprise { get; set; }
        
        public List<Vehicle>  Vehicles { get; set; } = new();

        public long? OnVehicleId { get; set; }
        
        public Vehicle? OnVehicle { get; set; }
        // could be active for one car
        // public bool IsActive { get; set; }
    }
}