using System.Collections.Generic;
using BaseTypes;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.Entities
{
    /// <summary>
    /// Транспортное средство
    /// </summary>
    public class Vehicle : BaseEntity
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
        
        /// <summary>
        /// Тип трансмиссии
        /// </summary>
        public Transmission Transmission { get; set; }
        
        public long BrandId { get; set; }
        
        public Brand Brand { get; set; }

        public long EnterpriseId { get; set; }

        public Enterprise Enterprise { get; set; }

        public List<Driver> Drivers { get; set; } = new();
        
        /// <summary>
        /// Active driver for current vehicle, null if there no active driver
        /// </summary>
        public Driver? ActiveDriver { get; set; }
    }
}