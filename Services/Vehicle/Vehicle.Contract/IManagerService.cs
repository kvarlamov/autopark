using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Contract.Dto;

namespace Vehicle.Contract
{
    public interface IManagerService
    {
        public Task<List<ManagerDto>> GetManagersAsync();
    }
}