using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.EventsHandlers;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;

public sealed class BeerBrewedConsumer : DomainEventConsumerBase<BeerBrewedEvent>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerBrewedEvent>> DomainEventsHanderAsync { get; }

    public BeerBrewedConsumer(IBeerService beerService,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory,
        messageSerializer)
    {
        DomainEventsHanderAsync = new List<IDomainEventHandlerAsync<BeerBrewedEvent>>
        {
            new BeerBrewedEventHandler(loggerFactory, beerService)
        };
    }
}