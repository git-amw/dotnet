using AMD.Services.Accounts.DomainLayer.DTOs;
using AMD.Services.Accounts.DomainLayer.Entities;
using AMD.Services.Accounts.DomainLayer.Interfaces;
using AutoMapper;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMD.Services.Account.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper mapper;
        private readonly IAuthRepo accountRepo;

        public AuthService(IMapper mapper, IAuthRepo accountRepo)
        {
            this.mapper = mapper;
            this.accountRepo = accountRepo;
        }

        public async Task<OnboardResponseEntity> CreateUserAccount(RegistrationDTO registrationDetails)
        {
            var data = mapper.Map<RegistrationEntity>(registrationDetails);
            var result = await accountRepo.CreateUserAccount(data);
            // SendMessage<int>(result.YourAccountNumber);
            SendMessage(result);
            return result;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("AccountNumberAutomation", true, false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "AccountNumberAutomation", body: body);
        }

        public async Task<string> UserLogin(LoginDTO loginDetails)
        {
            var data = mapper.Map<LoginEntity>(loginDetails);
            return await accountRepo.UserLogin(data);
        }
    }
}
