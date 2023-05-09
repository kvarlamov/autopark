﻿using System;
using BaseTypes;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.Entities
{
    /// <summary>
    /// Транспортное средство
    /// </summary>
    public class Vehicle : BaseModel
    {
        /// <summary>
        /// Стоимость транспортного средства
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Год выпуска транспортного средства
        /// </summary>
        public int ManufactureYear { get; set; }
        
        /// <summary>
        /// Пробег
        /// </summary>
        public int Mileage { get; set; }
        
        /// <summary>
        /// Цвет
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Состояние транспортного средства
        /// </summary>
        public VehicleState VehicleState { get; set; }
        
        public Transmission Transmission { get; set; }
    }
}