using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqTestConsumer.Data;
using RabbitMqTestConsumer.Models;
using Serilog.Context;

namespace RabbitMqTestConsumer.Class
{
    public static class ConsumerHelper
    {
        public static Consumer Consumer { get; set; }
    }
    public class Consumer
    {
        //private readonly ElasticClient _elasticClient;
        private readonly ILogger<Consumer> _logger;

        public Consumer(/*ElasticClient elasticClient,*/ ILogger<Consumer> logger)
        {
            //_elasticClient = elasticClient;
            _logger = logger;
        }
        public static IModel channel { get; set; }
        public static AsyncEventingBasicConsumer Co { get; set; }

        public void Consume()
        {

            if (channel == null)
            {
                var factory = new ConnectionFactory() { HostName = "172.16.20.14", UserName = "admin", Password = "12345678", DispatchConsumersAsync = true };
                var connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: "TestQ",
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
            channel.BasicConsume(queue: "TestQ",
                autoAck: true,
                consumer: Co);
        }




    }


}
