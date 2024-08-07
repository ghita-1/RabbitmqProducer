using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace RabbitmqProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IModel _channel;

        public MessageController(IModel channel)
        {
            _channel = channel;
            _channel.QueueDeclare(queue: "Messages", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] string message)
        {
            Byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "Messages", basicProperties: null, body: body);

            return Ok("Message received and sent to RabbitMQ");
        }
    }
}
