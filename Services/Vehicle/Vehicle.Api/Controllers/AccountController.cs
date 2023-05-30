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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null || (model.UserName is null && model.Email is null))
                return BadRequest(new { errorText = "Invalid username or password." });

            if (model.UserName is null)
                model.UserName = model.Email;
            
            var checkCredentialsResult = await _accountService.CheckLogin(model);

            if (checkCredentialsResult == null)
            {
                // Handle invalid login credentials
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            
            var token = JwtService.CreateJwtToken(checkCredentialsResult);

            if (model.ReturnUrl != null)
            {
                return Redirect($"{model.ReturnUrl}/?accessToken={token}");
            }
            
            return Redirect($"https://localhost:6003/vehicle?accessToken={token}");
        }
    }
}