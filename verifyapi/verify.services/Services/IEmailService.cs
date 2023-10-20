using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using verify.services.Models;

namespace verifyapi.Services
{
    public interface IEmailService
    {
        Task SendVerificationCodeAsync(string email, string code);

        Task AccountUpdatesOnEmail(ActivationEntity activationDetails);

        Task SendDepositAlertEmail(AlertEntity alertMessage);

        Task SendWithdrawAlertEmail(AlertEntity alertMessage);
    }
}