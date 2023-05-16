using System.Collections.Generic;
using BaseTypes;

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

        public long Enterprise { get; set; }
        
        public List<long>  Vehicles { get; set; }

        public long? OnVehicle { get; set; }
    }
}