using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitTest.Models;

namespace RabbitTest.Class
{
    public class Sender
    {
        public void Publish(string message)
        {
            var factory = new ConnectionFactory() { HostName = "172.16.20.14", UserName = "admin", Password = "12345678" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var model = message;
            var json = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "",
                routingKey: "TestQ",
                basicProperties: null,
                body: body);
        }
    }
}
