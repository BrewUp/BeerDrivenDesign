using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public abstract class ProductionDomainEventHandler<T> : IDomainEventHandlerAsync<T> where T : class, IDomainEvent
{
    protected ILogger Logger;

    protected ProductionDomainEventHandler(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }

    public abstract Task HandleAsync(T @event, CancellationToken cancellationToken = new());
}