using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consumer(IModel channel)
        { 
           
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
           
            channel.QueueDeclare("demo-fanout-queue",
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null
             );

            //var header = new Dictionary<string, object> { { "account", "new"} };
            channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", string.Empty);

            channel.BasicQos(0, 10, false);

             var consumer = new EventingBasicConsumer(channel);
           
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-fanout-queue", true, consumer);
            Console.WriteLine("Consumer Start");
            Console.ReadLine();

        }
    }
}
