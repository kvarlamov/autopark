using System.Collections.Generic;
using BaseTypes;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class Enterprise : BaseEntity
    {
        public string Name { get; set; }
        
        public string City { get; set; }

        public int Code { get; set; }

        public int NumberOfStaff { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<Driver> Drivers { get; set; }
    }
}