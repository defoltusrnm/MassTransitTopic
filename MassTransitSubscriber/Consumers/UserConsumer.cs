using MassTransit;
using MassTransitShared;

namespace MassTransitSubscriber.Consumers
{
    public class UserConsumer : IConsumer<User>
    {
        public Task Consume(ConsumeContext<User> context)
        {
            return Task.CompletedTask;
        }
    }
}
