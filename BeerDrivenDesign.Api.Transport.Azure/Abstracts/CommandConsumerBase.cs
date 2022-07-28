using MassTransit;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Api.Transport.Azure.Abstracts;

public abstract class CommandConsumerBase<TCommand> : IConsumer<TCommand> where TCommand : Command
{
    protected abstract ICommandHandlerAsync<TCommand> HandlerAsync { get; }
    public abstract Task Consume(ConsumeContext<TCommand> context);
}