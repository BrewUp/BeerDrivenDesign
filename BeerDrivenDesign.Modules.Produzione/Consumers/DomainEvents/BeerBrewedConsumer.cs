using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;

public sealed class BeerBrewedConsumer : DomainEventConsumerBase<BeerBrewedEvent>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerBrewedEvent>> DomainEventsHanderAsync { get; }

    public BeerBrewedConsumer(IDomainEventHandlerFactoryAsync domainEventHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory,
        messageSerializer)
    {
        DomainEventsHanderAsync = domainEventHandlerFactoryAsync.CreateDomainEventHandlersAsync<BeerBrewedEvent>();
    }
}