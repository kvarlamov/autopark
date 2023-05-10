using System;
using BaseTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public Guid BrandId { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// Состояние транспортного средства
        /// </summary>
        public VehicleState VehicleState { get; set; }
        
        /// <summary>
        /// Тип трансмиссии
        /// </summary>
        public Transmission Transmission { get; set; }
    }
}