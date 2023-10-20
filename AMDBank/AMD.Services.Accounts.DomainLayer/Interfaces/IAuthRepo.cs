using AMD.Services.Accounts.DomainLayer.Entities;
using System.Threading.Tasks;

namespace AMD.Services.Accounts.DomainLayer.Interfaces
{
    public interface IAuthRepo
    {
        Task<string> UserLogin(LoginEntity loginDetails);
        Task<OnboardResponseEntity> CreateUserAccount(RegistrationEntity registrationDetails);
    }
}
