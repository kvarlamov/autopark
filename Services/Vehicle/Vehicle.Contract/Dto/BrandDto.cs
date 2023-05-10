using BaseTypes;
using Vehicle.Contract.Enums;

namespace Vehicle.Contract.Dto
{
    public class BrandDto : BaseDto
    {
        /// <summary>
        /// Название бренда
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
    }
}