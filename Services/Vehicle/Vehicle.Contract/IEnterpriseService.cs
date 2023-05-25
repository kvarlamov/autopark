using System.Collections.Generic;
using System.Threading.Tasks;
using Enterprise.Contract.Dto;

namespace Vehicle.Contract
{
    public interface IEnterpriseService
    {
        Task<List<EnterpriseDto>> GetEnterprisesAsync();
        
        Task<List<EnterpriseDto>> GetEnterprisesByIdsAsync(List<long> ids);

        Task<EnterpriseDto> GetEnterprise(long id);
    }
}