using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using verify.services.Models;

namespace verifyapi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendDepositAlertEmail(AlertEntity alertMessage)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailConfig.From, _emailConfig.From);
                message.To.Add(new MailAddress(alertMessage.UserEmail));
                message.Subject = "Account update for you Infinity bank A/c";
                message.Body = "Dear Customer, <br /> <br />" + $"Rs.{alertMessage.Amount}.00 has been Credited to your account {MaskAccountNumber(alertMessage.AccountNumber.ToString())} <br /> <br /> <br /> If you have not done this transaction, please immediately call on 18002586161 to report this transaction <br /> <br /> Regards, <br /> INFINITY Bank Ltd.";
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                }
            }
        }

        public async Task SendWithdrawAlertEmail(AlertEntity alertMessage)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailConfig.From, _emailConfig.From);
                message.To.Add(new MailAddress(alertMessage.UserEmail));
                message.Subject = "Account update for you Infinity bank A/c";
                message.Body = "Dear Customer, <br /> <br />" + $"Rs.{alertMessage.Amount}.00 has been Debited from your account {MaskAccountNumber(alertMessage.AccountNumber.ToString())} <br /> <br /> <br /> If you have not done this transaction, please immediately call on 18002586161 to report this transaction <br /> <br /> Regards, <br /> INFINITY Bank Ltd.";
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                }
            }
        }
        private string MaskAccountNumber(string accountNumber)
        {

            var firstDigits = accountNumber.Substring(0, 1);

            var lastDigits = accountNumber.Substring(accountNumber.Length - 2, 2);

            var requiredMask = new String('X', accountNumber.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);

            return maskedString;
        }

        public async Task AccountUpdatesOnEmail(ActivationEntity activationDetails)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailConfig.From, _emailConfig.From);
                message.To.Add(new MailAddress(activationDetails.UserEmail));
                message.Subject = "Account is Activated";
                message.Body = "Dear Customer, <br />" + activationDetails.WelcomeMessage + "<br /> <br />" + "Bank IFSC : " + activationDetails.BankIFSC + "<br /> <br />" + "Your Account Number : " + activationDetails.YourAccountNumber + "<br /> <br /> Warm Regards, <br /> INFINITY Bank Ltd.";
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                }
            }
        }

        public async Task SendVerificationCodeAsync(string email, string code)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_emailConfig.From, _emailConfig.From);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "IMPORTANT!! Verification Code";
                    message.Body = $"Your verification code is: {code}" + "<br /> <br /> Warm Regards, <br /> INFINITY Bank Ltd.";
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        await smtp.SendMailAsync(message);
                    }
                }

                Console.WriteLine($"Verification code sent to {email} successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send verification code to {email}. Error: {ex.Message}");
            }
        }

        
    }
}
