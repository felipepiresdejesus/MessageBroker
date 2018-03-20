using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace MessageBroker.Publisher.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get(string message = "")
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

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                return new[] { $"Message sent: {message}" };
            }
        }
    }
}
