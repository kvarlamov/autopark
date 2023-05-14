using System.Collections.Generic;
using BaseTypes;
using Microsoft.Extensions.DependencyModel;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.Entities
{
    /// <summary>
    /// Бренд
    /// </summary>
    public class Brand : BaseEntity
    {
        /// <summary>
        /// Количество мест
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип транспортного средства
        /// </summary>
        public VehicleType VehicleType { get; set; }
        
        /// <summary>
        /// Бак
        /// </summary>
        public int Tank { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public int LoadCapacity { get; set; }
        
        /// <summary>
        /// Количество посадочных мест
        /// </summary>
        public int NumberOfSeats { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new();
    }
}