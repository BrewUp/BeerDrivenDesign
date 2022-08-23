using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;

public class BeerProductionCompletedConsumer : DomainEventConsumerBase<BeerProductionCompleted>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerProductionCompleted>> DomainEventsHanderAsync { get; }
    public BeerProductionCompletedConsumer(IDomainEventHandlerFactoryAsync domainEventHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory, IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration,
        loggerFactory, messageSerializer)
    {
        DomainEventsHanderAsync =
            domainEventHandlerFactoryAsync.CreateDomainEventHandlersAsync<BeerProductionCompleted>();
    }
}