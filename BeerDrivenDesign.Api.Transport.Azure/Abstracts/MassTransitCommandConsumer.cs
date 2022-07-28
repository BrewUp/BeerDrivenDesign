using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Api.Transport.Azure.Abstracts;

public abstract class MassTransitCommandConsumer<TCommand> : CommandConsumerBase<TCommand> where TCommand : Command
{
    protected readonly IServiceProvider ServiceProvider;
    protected readonly ILogger Logger;

    protected MassTransitCommandConsumer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
    {
        ServiceProvider = serviceProvider;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}