using BeerDrivenDesign.Modules.Produzione.Events;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class ProductionStartedEventHandler : IIntegrationEventHandlerAsync<ProductionStarted>
{
    public Task HandleAsync(ProductionStarted @event, CancellationToken cancellationToken = new())
    {
        

        return Task.CompletedTask;
    }
}