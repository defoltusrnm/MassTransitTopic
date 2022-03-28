using MassTransit;
using MassTransitShared;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitPublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IPublishEndpoint _publisher;
        private IBus _bus;

        public ValuesController(IPublishEndpoint publisher, IBus bus)
        {
            _publisher = publisher;
            _bus = bus;
        }
        
        [HttpGet]
        public async void Try()
        {
            //ISendEndpoint a = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/UserMessage123"));

            //await a.Send(new User { Login = "1", Password = "1" });

            await _publisher.Publish<User>(new User { Login = "1", Password = "1" });
        }
    }
}
