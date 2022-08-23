using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IProductionService _productionService;

    public BeerProductionStartedForProductionOrderEventHandler(ILoggerFactory loggerFactory, IProductionService productionService) : base(loggerFactory)
    {
        _productionService = productionService;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _productionService.CreateProductionOrderAsync(@event.BatchId, @event.BatchNumber, @event.BeerId,
                @event.BeerType, @event.Quantity, @event.ProductionStartTime);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}