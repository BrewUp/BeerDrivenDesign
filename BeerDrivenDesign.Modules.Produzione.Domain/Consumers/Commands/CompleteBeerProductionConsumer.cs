using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;

public sealed class CompleteBeerProductionConsumer : CommandConsumerBase<CompleteBeerProduction>
{
    protected override ICommandHandlerAsync<CompleteBeerProduction> HandlerAsync { get; }

    public CompleteBeerProductionConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlerAsync = new CompleteBeerProductionCommandHandler(repository, loggerFactory);
    }
}