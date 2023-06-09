﻿using System;
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

        public List<Vehicle> Vehicles { get; set; } = new();

        public List<Driver> Drivers { get; set; } = new();

        public List<Manager> Managers { get; set; } = new();
        public int? TimezoneOffset { get; set; }
    }
}