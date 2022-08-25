using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IProductionService _productionService;
    private readonly ProductionHub _productionHub;

    public BeerProductionStartedForProductionOrderEventHandler(ILoggerFactory loggerFactory,
        ProductionHub productionHub,
        IProductionService productionService) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionHub = productionHub;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _productionService.CreateProductionOrderAsync(@event.BatchId, @event.BatchNumber, @event.BeerId,
                @event.BeerType, @event.Quantity, @event.ProductionStartTime);

            await _productionHub.ProductionOrderUpdated(@event.BatchId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}