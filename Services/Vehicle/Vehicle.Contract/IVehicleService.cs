﻿using System.Collections.Generic;
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
        /// <returns>Список транспортных средств</returns>
        Task<List<VehicleDto>> GetVehicles();
        
        Task<List<VehicleDto>> GetVehiclesByIds(List<long> ids);
        
        Task<VehicleDto> GetVehicle(long id);

        Task<VehicleDto> CreateAsync(VehicleDto dto);

        Task<VehicleDto> UpdateAsync(VehicleDto dto);

        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// Получить все бренды
        /// </summary>
        /// <returns>Список брендов</returns>
        Task<List<BrandDto>> GetBrands();

        Task<List<VehicleDto>> GetVehicles(PaginationRequestDto request);
    }
}