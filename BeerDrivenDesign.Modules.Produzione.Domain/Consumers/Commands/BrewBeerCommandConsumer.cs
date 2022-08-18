using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Commands;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;

public sealed class BrewBeerCommandConsumer : CommandConsumerBase<BrewBeerCommand>
{
    protected override ICommandHandlerAsync<BrewBeerCommand> CommandHandlerAsync { get; }

    public BrewBeerCommandConsumer(ICommandHandlerFactoryAsync commandHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory,
        IMessageSerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        CommandHandlerAsync = commandHandlerFactoryAsync.CreateCommandHandlerAsync<BrewBeerCommand>();
    }
}