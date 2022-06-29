using System.Text;
using Domain.Models;
using Infrastructure.Configuration;
using Infrastructure.Services.Publisher;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog.Context;

namespace Service.Services.Publisher
{
    public class PublisherService : IPublisherService
    {
        #region Fields

        private readonly ILogger<PublisherService> _logger;

        #endregion

        #region Methods


        public async Task<bool> Publish(int number, CancellationToken cancellationToken)
        {
            var model = new PublishMessage(number);
            var json = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(json);
            var rabbitFactory = new ConnectionFactory()
            {
                HostName = RabbitConfiguration.Uri,
                UserName = RabbitConfiguration.Username,
                Password = RabbitConfiguration.Password
            };
            using var connection = rabbitFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: RabbitConfiguration.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.BasicPublish(exchange: "",
              routingKey: RabbitConfiguration.QueueName,
              basicProperties: null,
              body: body);
            using (LogContext.PushProperty("ConsumerContext", 0853))
            {
                _logger.LogInformation("message published => {0}", json);
            }

            return true;
        }


        #endregion

        #region Utilities



        #endregion

        #region Ctor


        public PublisherService(ILogger<PublisherService> logger)
        {
            _logger = logger;
        }

        #endregion

    }
}
