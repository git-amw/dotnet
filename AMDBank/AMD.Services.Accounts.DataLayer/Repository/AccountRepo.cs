using AMD.Services.Accounts.DomainLayer.Entities;
using AMD.Services.Accounts.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMD.Services.Accounts.DataLayer.Repository
{
    public class AccountRepo : IAuthRepo
    {
        private readonly UserManager<RegistrationEntity> userManager;
        private readonly SignInManager<RegistrationEntity> signInManager;
        private readonly OnboardingOptionsEntity onboardOption;
        private readonly JwtOptionsEntity jwtOptions;

        public AccountRepo(UserManager<RegistrationEntity> userManager, 
            SignInManager<RegistrationEntity> signInManager, IOptions<JwtOptionsEntity> jwtOptions,
            IOptions<OnboardingOptionsEntity> onboardOption)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.onboardOption = onboardOption.Value;
            this.jwtOptions = jwtOptions.Value;
        }

        public async Task<OnboardResponseEntity> CreateUserAccount(RegistrationEntity registrationDetails)
        {
            registrationDetails.UserName = registrationDetails.Email;
            await userManager.CreateAsync(registrationDetails, registrationDetails.Password);
            var userData = userManager.FindByNameAsync(registrationDetails.Email).Result;
            var response = new OnboardResponseEntity()
            {
                WelcomeMessage = onboardOption.WelcomeMessage,
                BankIFSC = onboardOption.BankIFSC,
                YourAccountNumber = userData.AccountNumber,
                UserEmail = registrationDetails.Email,
            };
            return response;
        }

        public async Task<string> UserLogin(LoginEntity loginDetails)
        {
            var result = await signInManager.PasswordSignInAsync(loginDetails.Email, loginDetails.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var token = GenerateToken(loginDetails);
            return token;
        }

        public string GenerateToken(LoginEntity loginDetails)
        {
            var userData = userManager.FindByNameAsync(loginDetails.Email).Result;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,loginDetails.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,  userData.FirstName + " " + userData.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, userData.AccountNumber.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOptions.Audience,
                Issuer = jwtOptions.Issuer,
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddMinutes(35),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
