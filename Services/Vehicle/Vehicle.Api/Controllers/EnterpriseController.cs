using System.Threading.Tasks;
using Enterprise.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vehicle.Contract;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly ILogger<EnterpriseController> _logger;

        public EnterpriseController(
            IEnterpriseService enterpriseService,
            ILogger<EnterpriseController> logger)
        {
            _enterpriseService = enterpriseService;
            _logger = logger;
        }
        
        
        public async Task<EnterpriseDto> Index()
        {
            return await _enterpriseService.GetEnterprisesAsync();
        }
    }
}