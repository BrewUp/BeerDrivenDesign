using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IProductionService _productionService;

    public BeerProductionCompletedEventHandler(ILoggerFactory loggerFactory, IProductionService productionService) : base(loggerFactory)
    {
        _productionService = productionService;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new ())
    {
        try
        {
            await _productionService.CompleteProductionOrderAsync(@event.BatchNumber, @event.Quantity,
                @event.ProductionCompleteTime);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}