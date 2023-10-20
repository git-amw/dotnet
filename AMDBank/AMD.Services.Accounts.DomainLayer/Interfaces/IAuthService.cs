using AMD.Services.Accounts.DomainLayer.DTOs;
using AMD.Services.Accounts.DomainLayer.Entities;
using System.Threading.Tasks;

namespace AMD.Services.Accounts.DomainLayer.Interfaces
{
    public interface IAuthService
    {
        Task<string> UserLogin(LoginDTO loginDetails);
        Task<OnboardResponseEntity> CreateUserAccount(RegistrationDTO registrationDetails);

    }
}
