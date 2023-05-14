using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Contract.Dto;

namespace Driver.Contract
{
    public interface IDriverService
    {
        Task<List<DriverDto>> GetDriversAsync();
    }
}