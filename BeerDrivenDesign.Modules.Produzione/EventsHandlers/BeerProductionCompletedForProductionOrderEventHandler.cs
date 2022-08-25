using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IBeerService _beerService;
    private readonly ProductionHub _productionHub;

    public BeerProductionCompletedForProductionOrderEventHandler(ILoggerFactory loggerFactory,
        IBeerService beerService,
        ProductionHub productionHub) : base(loggerFactory)
    {
        _beerService = beerService;
        _productionHub = productionHub;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new ())
    {
        try
        {
            await _beerService.UpdateBeerQuantityAsync(@event.BeerId, @event.Quantity);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}