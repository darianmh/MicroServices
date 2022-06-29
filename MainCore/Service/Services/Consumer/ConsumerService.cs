using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.Services.Consumer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog.Context;

namespace Service.Services.Consumer
{
    public class ConsumerService : IConsumerService
    {
        //private readonly ElasticClient _elasticClient;
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(/*ElasticClient elasticClient,*/ ILogger<ConsumerService> logger)
        {
            //_elasticClient = elasticClient;
            _logger = logger;
        }
        public static IModel channel { get; set; }
        public static AsyncEventingBasicConsumer Co { get; set; }

        public async Task Consume()
        {

            if (channel == null)
            {
                var factory = new ConnectionFactory() { HostName = RabbitConfiguration.Uri, UserName = RabbitConfiguration.Username, Password = RabbitConfiguration.Password, DispatchConsumersAsync = true };
                var connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: RabbitConfiguration.QueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }

            if (Co == null)
            {
                var consumer = new AsyncEventingBasicConsumer(channel);
                Co = consumer;
            }

            Co.Received += async (model, ea) =>
            {
                using (LogContext.PushProperty("ConsumerContext", 0853))
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($"{message} - {DateTime.Now:hhmm} - consumer");
                }

            };
            channel.BasicConsume(queue: RabbitConfiguration.QueueName,
                autoAck: true,
                consumer: Co);
        }



    }
}
