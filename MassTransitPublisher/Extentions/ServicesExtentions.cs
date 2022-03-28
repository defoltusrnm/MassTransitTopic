using MassTransit;
using MassTransitShared;
using RabbitMQ.Client;

namespace MassTransitPublisher.Extentions
{
    public static class ServicesExtentions
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {

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
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
