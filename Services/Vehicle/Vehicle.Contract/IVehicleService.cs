using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Contract.Dto;

namespace Vehicle.Contract
{
    /// <summary>
    /// Интерфейс сервиса транспортных средств
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Получить все транспопртные средства
        /// </summary>
        /// <returns></returns>
        Task<List<VehicleDto>> GetVehicles();
    }
}