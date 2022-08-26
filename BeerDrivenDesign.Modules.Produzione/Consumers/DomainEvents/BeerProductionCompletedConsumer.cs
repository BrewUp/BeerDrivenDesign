using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;

public class BeerProductionCompletedConsumer : DomainEventConsumerBase<BeerProductionCompleted>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerProductionCompleted>> HandlersAsync { get; }

    public BeerProductionCompletedConsumer(IDomainEventHandlerFactoryAsync domainEventHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration,
        loggerFactory, messageSerializer)
    {
        HandlersAsync =
            domainEventHandlerFactoryAsync.CreateDomainEventHandlersAsync<BeerProductionCompleted>();
    }
}