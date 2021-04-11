using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace System.Consumer.RabbitMQ.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsumerRabbtiMQ();
        }

        public static void ConsumerRabbtiMQ()
        {
            Console.WriteLine("Init Consumer!");

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "TesteRabbit", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine(message);
                    };

                    channel.BasicConsume(queue: "TesteRabbit", autoAck: true, consumer: consumer);

                    Console.ReadLine();
                }
            }

            Console.WriteLine("Finish Consumer!");
        }
    }
}
