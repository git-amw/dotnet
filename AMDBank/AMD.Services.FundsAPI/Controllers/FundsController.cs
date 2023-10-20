using AMD.Services.Funds.DomainLayer.DTO;
using AMD.Services.Funds.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMD.Services.FundsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FundsController : ControllerBase
    {
        private readonly IFunds funds;
        private readonly IAlert alert;

        public FundsController(IFunds funds, IAlert alert)
        {
            this.funds = funds;
            this.alert = alert;
        }

        [HttpPost]
        [Route("Deposit")]
        public async Task<IActionResult> Deposit(TransactionDTO transactionData)
        {
            if (ModelState.IsValid)
            {
                int userAccountNumber = GetAccountNumber();
                var result = await funds.DepositMoney(transactionData, userAccountNumber);
                string email = GetUserEmail();
                await alert.DepositAlert(email, result.Amount, userAccountNumber);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Withdraw")]
        public async Task<IActionResult> Withdraw(TransactionDTO transactionData)
        {   
            if (ModelState.IsValid)
            {
                int userAccountNumber = GetAccountNumber();
                var result = await funds.WithdrawMoney(transactionData, userAccountNumber);
                if (result.Amount == -1)
                {
                    return BadRequest("Insufficient Balance, Transaction doesnot Complete!");
                }
                string email = GetUserEmail();
                await alert.WithdrawAlert(email, result.Amount, userAccountNumber);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("CheckBalance")]
        public async Task<IActionResult> CheckBalance()
        {
            int userAccountNumber = GetAccountNumber();
            var result = await funds.CheckBalance(userAccountNumber);
            return Ok(result);
        }

        [HttpGet]
        [Route("TransactionHistory")]
        public async Task<IActionResult> TransactionHistory()
        {
            int userAccountNumber = GetAccountNumber();
            var result = await funds.TransactionHistory(userAccountNumber);
            if (result.Count == 0)
            {
                return Ok("Account doesn't have Transaction History");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Health")]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok("Funds API is live");
        }

        private int GetAccountNumber()
        {
            var userIdentity = (User.Identity as ClaimsIdentity);
            var result = userIdentity.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return  Convert.ToInt32(userIdentity.FindFirst(JwtRegisteredClaimNames.Jti).Value);
        }

        private string GetUserEmail()
        {
            var userIdentity = (User.Identity as ClaimsIdentity);
            var result = userIdentity.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return result;
        }
    }
}
