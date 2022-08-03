using BeerDrivenDesign.Api.Transport.RabbitMq.Consumers.Commands;
using BeerDrivenDesign.Api.Transport.RabbitMq.Consumers.Events;
using BeerDrivenDesign.Api.Transport.RabbitMq.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone;

namespace BeerDrivenDesign.Api.Transport.RabbitMq;

public static class RmqHelper
{
    public static IServiceCollection AddRmqTransport(this IServiceCollection services, RabbitMqSettings rmqSettings)
    {
        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(rmqSettings.BrokerUrl, host =>
            {
                host.Username(rmqSettings.Login);
                host.Password(rmqSettings.Password);
            });

            var loggerFactory = provider.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
            var localProvider = services.BuildServiceProvider();

            cfg.ReceiveEndpoint(rmqSettings.QueueName, endpoint =>
            {
                endpoint.Bind(rmqSettings.ExchangeName);
                endpoint.PrefetchCount = 16;
                endpoint.UseMessageRetry(x => x.Interval(2, 100));

                #region CommandsConsumers
                endpoint.Consumer(() => new BrewBeerCommandConsumer(localProvider, loggerFactory));
                #endregion

                #region EventsConsumers
                endpoint.Consumer(() => new ProductionStartedConsumer(localProvider));
                #endregion
            });
        }));

        services.AddSingleton<IServiceBus, ServiceBus>();
        services.AddSingleton<IEventBus, ServiceBus>();
        services.AddSingleton<IHostedService, ServiceBus>();

        return services;
    }
}