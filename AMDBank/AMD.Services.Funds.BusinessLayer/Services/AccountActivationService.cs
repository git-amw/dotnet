using AMD.Services.Funds.DataLayer.Data;
using AMD.Services.Funds.DomainLayer.Entites;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AMD.Services.Funds.BusinessLayer.Services
{
    public class AccountActivationService : BackgroundService
    {
        private readonly ConnectionFactory _factory;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IHttpClientFactory httpClientFactory;
        private IConnection _connection;
        private IModel _channel;

        public AccountActivationService(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("AccountNumberAutomation", true, false);
            this.scopeFactory = scopeFactory;
            this.httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            await Task.Run(() =>
            {
                int accountNumber = 0;
                consumer.Received += (model, ea) =>
                {
                    
                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString(body.ToArray());

                    var incomingMessage = JsonSerializer.Deserialize<IncomingMessageEntity>(message);

                    accountNumber = incomingMessage.YourAccountNumber;

                    var scope = scopeFactory.CreateScope();

                    Console.WriteLine("Recieved", accountNumber);

                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var result = new FundsEntity()
                    {
                        AccountNumber = accountNumber, 
                        AccountBalance = 0
                    };
                    
                    dbContext.FundsTable.AddAsync(result);
                    dbContext.SaveChangesAsync();

                    HttpClient client = httpClientFactory.CreateClient();

                    HttpRequestMessage OutGoingMessage = new HttpRequestMessage
                    {
                        Content = new StringContent(JsonSerializer.Serialize(incomingMessage), Encoding.UTF8, "application/json"),
                        RequestUri = new Uri("http://localhost:7000/gateway/ActivationUpdate"),
                        Method = HttpMethod.Post,
                    };
                    client.Send(OutGoingMessage);
                };
            });

            _channel.BasicConsume(queue: "AccountNumberAutomation", autoAck: true, consumer: consumer);

           
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
    }
}
