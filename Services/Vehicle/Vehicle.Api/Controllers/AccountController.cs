using System.Threading.Tasks;
using AutoPark.Api.Authentication;
using AutoPark.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Contract;

namespace AutoPark.Api.Controllers
{
    // todo IMPORTANT need to move to separate Identity project!
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        [Consumes("application/x-www-form-urlencoded", "application/json")]
        public async Task<IActionResult> Token([FromForm] LoginViewModel dto)
        {
            if (dto == null || (dto.UserName is null && dto.Email is null))
                return BadRequest(new { errorText = "Invalid username or password." });

            if (dto.UserName is null)
                dto.UserName = dto.Email;
            
            var checkCredentialsResult = await _accountService.CheckLogin(dto);
            
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