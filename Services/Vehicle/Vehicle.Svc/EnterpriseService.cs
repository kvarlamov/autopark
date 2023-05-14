using System.Threading.Tasks;
using Enterprise.Contract.Dto;
using Vehicle.Contract;

namespace AutoPark.Svc
{
    public class EnterpriseService : IEnterpriseService
    {
        public Task<EnterpriseDto> GetEnterprisesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}