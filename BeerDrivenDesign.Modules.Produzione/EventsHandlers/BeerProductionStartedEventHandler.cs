using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedEventHandler : IDomainEventHandlerAsync<BeerProductionStarted>
{
    private readonly IBeerService _beerService;
    private readonly ILogger _logger;

    public BeerProductionStartedEventHandler(ILoggerFactory loggerFactory, IBeerService beerService)
    {
        _beerService = beerService;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _beerService.CreateBeerAsync(@event.BeerId, @event.Quantity, @event.BeerType, @event.BatchId,
                @event.ProductionStartTime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}