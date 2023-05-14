using System;

namespace WebClient.Shared
{
    public class VehicleDto
    {
        public long Id { get; set; }
        
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
        public long BrandId { get; set; }
    }
    
    public enum Transmission
    {
        Manual = 1,
        Automatic = 2
    }

    public enum VehicleState
    {
        Normal = 1,
        NeedsRepair = 2
    }
}