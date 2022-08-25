using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IProductionService _productionService;
    private readonly ProductionHub _productionHub;

    public BeerProductionCompletedEventHandler(ILoggerFactory loggerFactory,
        IProductionService productionService,
        ProductionHub productionHub) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionHub = productionHub;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new ())
    {
        try
        {
            await _productionService.CompleteProductionOrderAsync(@event.BatchNumber, @event.Quantity,
                @event.ProductionCompleteTime);

            await _productionHub.ProductionOrderUpdated(new BatchId(@event.AggregateId.Value));
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}