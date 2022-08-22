using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;

public sealed class StartBeerProductionConsumer : CommandConsumerBase<StartBeerProduction>
{
    protected override ICommandHandlerAsync<StartBeerProduction> CommandHandlerAsync { get; }

    public StartBeerProductionConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration,
        loggerFactory, messageSerializer)
    {
        CommandHandlerAsync = new StartBeerProductionCommandHandler(repository, loggerFactory);
    }
}