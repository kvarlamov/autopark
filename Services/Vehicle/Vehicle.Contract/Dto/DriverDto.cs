using System.Collections.Generic;
using BaseTypes;
using Enterprise.Contract.Dto;
using Vehicle.Contract.Dto;

namespace Driver.Contract.Dto
{
    public class DriverDto : BaseDto
    {
        /// <summary>
        /// First Name + Last Name
        /// </summary>
        public string Name { get; set; }

        public int Age { get; set; }

        public decimal Salary { get; set; }

        public long EnterpriseId { get; set; }

        public EnterpriseDto Enterprise { get; set; }

        public long VehicleId { get; set; }
        
        public List<VehicleDto>  Vehicle { get; set; }

        // could be actie for one car
        public bool IsActive { get; set; }
    }
}