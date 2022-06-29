// See https://aka.ms/new-console-template for more information

using System.Text;
using Infrastructure.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory()
{
    HostName = RabbitConfiguration.Uri,
    Password = RabbitConfiguration.Password,
    UserName = RabbitConfiguration.Username
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
try
{
    channel.QueueDeclare(queue: RabbitConfiguration.QueueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);
}
catch (Exception e)
{
    //ignore
}

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [x] Received {0}", message);
};
channel.BasicConsume(queue: RabbitConfiguration.QueueName,
    autoAck: true,
    consumer: consumer);
Console.ReadLine();
