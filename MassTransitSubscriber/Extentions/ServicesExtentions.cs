using MassTransit;
using MassTransitShared;
using MassTransitSubscriber.Consumers;
using RabbitMQ.Client;

namespace MassTransitSubscriber.Extentions
{
    public static class ServicesExtentions
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserConsumer>();
                x.AddConsumer<UserConsumer2>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    //cfg.Send<User>(x => { x.UseRoutingKeyFormatter(context => "routingKey"); });
                    cfg.Message<User>(x => x.SetEntityName("UserMessage123"));
                    cfg.Publish<User>(x => { x.ExchangeType = ExchangeType.Topic; });
                    cfg.ReceiveEndpoint("UserQueue123", e =>
                    {
                        e.ConfigureConsumer<UserConsumer>(context);
                        e.Bind("UserMessage123", x =>
                        {
                            x.ExchangeType = ExchangeType.Topic;
                        });
                    });
                    cfg.ReceiveEndpoint("UserQueue2123", e =>
                    {
                        e.ConfigureConsumer<UserConsumer2>(context);
                        e.Bind("UserMessage123", x =>
                        {
                            x.ExchangeType = ExchangeType.Topic;
                        });
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
