// See https://aka.ms/new-console-template for more information



using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabitMQ.Consumer;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

 using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

FanoutExchangeConsumer.Consumer(channel);
