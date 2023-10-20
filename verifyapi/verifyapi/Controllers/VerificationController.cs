using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using verify.DataLayer.Interfaces;
using verify.services.Models;
using verifyapi.Models;
using verifyapi.Services;

namespace verifyapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IValidation validate;
        private static readonly List<string> _verificationCodes = new List<string>();

        public VerificationController(IEmailService emailService, IValidation validate)
        {
            _emailService = emailService;
            this.validate = validate;
        }

        [HttpGet]
        [Route("Health")]
        public IActionResult HealthCheck()
        {
            return Ok("Verify API is live");
        }

        [HttpPost]
        [Route("DepositAlert")]
        public async Task<IActionResult> SendDepositAlert(AlertEntity alertMessage)
        {
            Console.WriteLine("herer");
            Console.WriteLine(alertMessage.AccountNumber);
            await _emailService.SendDepositAlertEmail(alertMessage);
            return Ok();
        }

        [HttpPost]
        [Route("WithdrawAlert")]
        public async Task<IActionResult> SendWithdrawAlert(AlertEntity alertMessage)
        {
            await _emailService.SendWithdrawAlertEmail(alertMessage);
            return Ok();
        }

        [HttpPost]
        [Route("ActivateAccount")]
        public async Task<IActionResult> AccountActivation(ActivationEntity incomingMessage)
        {
            await _emailService.AccountUpdatesOnEmail(incomingMessage);
            return Ok();
        }

        [HttpGet]
        [Route("VerifyAadhaar")]
        public async Task<IActionResult> ValidateAadhaar([FromQuery] string aadhaarNumber)
        {
            if (aadhaarNumber.Length == 0)
            {
                return BadRequest();
            }
            var result = await validate.VerifyAadhaar(aadhaarNumber);
            if (result)
            {
                return Ok("Valid");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("VerifyPAN")]
        public async Task<IActionResult> ValidatePAN([FromQuery] string pan)
        {
            if (pan.Length == 0)
            {
                return BadRequest();
            }
            var result = await validate.VerifyPAN(pan);
            if (result)
            {
                return Ok("Valid");
            }
            return NotFound();
        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> SendVerificationCode(VerificationDTO verify)
        {
            var code = GenerateVerificationCode();

            _verificationCodes.Add(code);

            await _emailService.SendVerificationCodeAsync(verify.Email, code);

            return Ok("Verification code sent successfully");
        }

        [HttpPost("VerifyOtp")]
        public IActionResult VerifyEmail(OTP data)
        {
            if (_verificationCodes.Contains(data.Code))
            {
                _verificationCodes.Remove(data.Code);
                return Ok("Email verified successfully");
               
            }
            return BadRequest("Invalid verification code");
        }

        private string GenerateVerificationCode()
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
