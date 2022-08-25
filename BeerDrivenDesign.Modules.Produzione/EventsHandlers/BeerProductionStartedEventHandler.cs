using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IBeerService _beerService;
    private ProductionHub _productionHub;

    public BeerProductionStartedEventHandler(ILoggerFactory loggerFactory,
        IBeerService beerService,
        ProductionHub productionHub) : base(loggerFactory)
    {
        _beerService = beerService;
        _productionHub = productionHub;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _beerService.CreateBeerAsync(@event.BeerId, @event.BeerType, @event.BatchId,
                @event.ProductionStartTime);

            await _productionHub.ProductionOrderUpdated(@event.BatchId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}