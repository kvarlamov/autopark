using System;
using BaseTypes;
using Vehicle.Contract.Enums;

namespace Vehicle.Contract.Dto
{
    public class VehicleDto : BaseDto
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
        
        public string BrandName { get; set; }
        public Guid BrandId { get; set; }
    }
}