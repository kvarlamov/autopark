using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPark.Api.Authentication;
using Enterprise.Contract.Dto;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthSchemas.Jwt)]
        [Authorize(Policy = Policies.IsManager)]
        public async Task<List<EnterpriseDto>> Index()
        {
            return await _enterpriseService.GetEnterprisesAsync();
        }
    }
}