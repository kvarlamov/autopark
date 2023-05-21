using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoPark.Api.Authentication;
using AutoPark.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vehicle.Contract;

namespace AutoPark.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest(new { errorText = "Invalid username or password." });
            
            var checkCredentialsResult = await _accountService.CheckLogin(dto);
            
            //todo - раскомментить когда сделаю нормально с ролями
            if (checkCredentialsResult == null)
                return BadRequest(new { errorText = "Invalid username or password." });
            
            var token = JwtService.CreateJwtToken(checkCredentialsResult);
 
            var response = new
            {
                access_token = token
            };

            return new JsonResult(response);
        }
        
    }
}