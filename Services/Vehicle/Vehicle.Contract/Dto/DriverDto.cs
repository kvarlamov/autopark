﻿using System.Collections.Generic;
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

        public long VehicleId { get; set; }
        
        public List<long>  Vehicles { get; set; }

        // could be active for only one car
        public bool IsActive { get; set; }
    }
}