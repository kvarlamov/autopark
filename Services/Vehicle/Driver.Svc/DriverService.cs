using System.Threading.Tasks;
using Driver.Contract;
using Driver.Contract.Dto;

namespace Driver.Svc
{
    public class DriverService : IDriverService
    {
        public DriverService()
        {
            
        }
        
        public Task<DriverDto> GetDriversAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}