using AMD.Services.Accounts.DomainLayer.DTOs;
using AMD.Services.Accounts.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMD.Services.AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService accountService;

        public AccountController(IAuthService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationDTO registrationDetails)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.CreateUserAccount(registrationDetails);
                return Created("Account is opened successfully", result);

            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDetials)
        {
            if (ModelState.IsValid)
            {
                string result = await accountService.UserLogin(loginDetials);
                if (string.IsNullOrEmpty(result))
                {
                    return NotFound("Email or Password does not match");
                }
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Health")]
        public IActionResult HealthCheck()
        {
            return Ok("Account API is live");
        }
    }
}
