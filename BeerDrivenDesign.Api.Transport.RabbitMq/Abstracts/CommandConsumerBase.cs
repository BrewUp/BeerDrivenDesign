using BrewUp.Shared.Messages.Commands;
using MassTransit;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Api.Transport.RabbitMq.Abstracts;

public abstract class CommandConsumerBase<TCommand> : IConsumer<TCommand> where TCommand : MassCommand
{
    protected abstract ICommandHandlerAsync<TCommand> HandlerAsync { get; }
    public abstract Task Consume(ConsumeContext<TCommand> context);
}