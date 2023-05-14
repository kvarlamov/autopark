﻿using System.Collections.Generic;
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

        public long VehicleId { get; set; }
        
        public List<Vehicle>  Vehicle { get; set; }

        // could be actie for one car
        public bool IsActive { get; set; }
    }
}