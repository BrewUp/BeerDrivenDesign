using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;

public sealed class BrewBeerCommandConsumer : CommandConsumerBase<BrewBeerCommand>
{
    protected override ICommandHandlerAsync<BrewBeerCommand> CommandHandlerAsync { get; }

    public BrewBeerCommandConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory,
        IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        CommandHandlerAsync = new BrewBeerCommandHandler(repository, loggerFactory);
    }
}