using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IProductionService _productionService;
    private readonly IProductionBroadcastService _productionBroadcastService;

    public BeerProductionCompletedForProductionOrderEventHandler(ILoggerFactory loggerFactory,
        IProductionService productionService,
        IProductionBroadcastService productionBroadcastService) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionBroadcastService = productionBroadcastService;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _productionService.CompleteProductionOrderAsync(@event.BatchNumber, @event.Quantity,
                @event.ProductionCompleteTime);

            await _productionBroadcastService.PublishProductionOrderUpdatedAsync(new BatchId(@event.AggregateId.Value));
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}