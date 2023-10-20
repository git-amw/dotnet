using AMD.Services.Funds.DomainLayer.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMD.Services.Funds.BusinessLayer.Services
{
    public class OutGoingMessage
    {
        public string UserEmail { get; set; }

        public int Amount { get; set; }

        public int AccountNumber { get; set; }
    }
    public class AlertService : IAlert
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly HttpClient client;

        public AlertService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;

            client = httpClientFactory.CreateClient();
        }

        public async Task DepositAlert(string useremail, int amount, int userAccountNumber)
        {
            OutGoingMessage data = new OutGoingMessage()
            {
               UserEmail = useremail,
               Amount = amount,
               AccountNumber = userAccountNumber
            };
            HttpRequestMessage OutGoingMessage = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"),
                RequestUri = new Uri("http://localhost:7000/gateway/DepositAlert"),
                Method = HttpMethod.Post,
            };
            await client.SendAsync(OutGoingMessage);
        }

        public async Task WithdrawAlert(string useremail, int amount, int userAccountNumber)
        {
            OutGoingMessage data = new OutGoingMessage()
            {
                UserEmail = useremail,
                Amount = amount,
                AccountNumber = userAccountNumber
            };
            HttpRequestMessage OutGoingMessage = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"),
                RequestUri = new Uri("http://localhost:7000/gateway/WithdrawAlert"),
                Method = HttpMethod.Post,
            };
            await client.SendAsync(OutGoingMessage);
        }
    }
}
