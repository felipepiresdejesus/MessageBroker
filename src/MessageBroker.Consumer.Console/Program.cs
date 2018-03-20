using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using C = System.Console;
namespace Lab.MessageBroker.Consumer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                C.WriteLine("Press [enter] to exit.");
                C.ReadLine();
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            C.WriteLine("[x] Received {0}", message);
        }
    }
}
