using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabitMQ.Producer
{
    public static class DireectExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-messagee--ttl",30000 }
            };
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct,arguments: ttl);

            var count = 0;
            while (true)
            {
                var message = new { name = "product", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("demo-direct-exchange", "account-init", null, body);
                count++;
                Thread.Sleep(1000);
            }

        }
    }
}
