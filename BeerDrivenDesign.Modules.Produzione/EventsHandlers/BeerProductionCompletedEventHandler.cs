using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IBeerService _beerService;

    public BeerProductionCompletedEventHandler(ILoggerFactory loggerFactory,
        IBeerService beerService) : base(loggerFactory)
    {
        _beerService = beerService;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new())
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