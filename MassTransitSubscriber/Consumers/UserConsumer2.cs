using MassTransit;
using MassTransitShared;

namespace MassTransitSubscriber.Consumers
{
    public class UserConsumer2 : IConsumer<User>
    {
        public Task Consume(ConsumeContext<User> context)
        {
            return Task.CompletedTask;
        }
    }
}
