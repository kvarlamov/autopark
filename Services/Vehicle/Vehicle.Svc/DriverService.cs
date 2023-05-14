using System.Threading.Tasks;
using Driver.Contract;
using Driver.Contract.Dto;

namespace AutoPark.Svc
{
    public class DriverService : IDriverService
    {
        public Task<DriverDto> GetDriversAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}