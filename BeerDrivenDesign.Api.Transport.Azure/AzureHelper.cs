using BeerDrivenDesign.Api.Transport.Azure.Consumers;
using BeerDrivenDesign.Api.Transport.Azure.Settings;
using BeerDrivenDesign.Modules.Produzione.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone;

namespace BeerDrivenDesign.Api.Transport.Azure;

public static class AzureHelper
{
    public static IServiceCollection AddAzureTransport(this IServiceCollection services, ServiceBusOptions serviceBusOptions)
    {
        // create the bus using Azure Service bus
        var azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
        {
            busFactoryConfig.Host(serviceBusOptions.ConnectionString);

            // specify the message Purchase to be sent to a specific topic
            busFactoryConfig.Message<ProductionStarted>(configTopology =>
            {
                configTopology.SetEntityName(nameof(ProductionStarted).ToLower());
            });

        });

        services.AddSingleton(provider => Bus.Factory.CreateUsingAzureServiceBus(cfg =>
        {
            var loggerFactory = provider.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
            var localProvider = services.BuildServiceProvider();

            cfg.Host(serviceBusOptions.ConnectionString);

            cfg.ReceiveEndpoint(serviceBusOptions.QueueName, endpoint =>
            {
                //    endpoint.PrefetchCount = 100;
                //    endpoint.MaxConcurrentCalls = 100;
                //    endpoint.LockDuration = TimeSpan.FromMinutes(5);
                //    endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(30);

                //    endpoint.UseMessageRetry(x => x.Interval(2, 100));

                //    //cfg.Publish<ProductionStarted>(x =>
                //    //{
                //    //    x.EnablePartitioning = true;
                //    //});

                cfg.SubscriptionEndpoint<ProductionStarted>("production-subscription", endpoint =>
                {
                    endpoint.Consumer(() => new ProductionStartedConsumer(localProvider));
                });
            });
        }));

        services.AddSingleton<IServiceBus, ServiceBus>();
        services.AddSingleton<IEventBus, ServiceBus>();
        services.AddSingleton<IHostedService, ServiceBus>();

        return services;
    }
}