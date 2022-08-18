using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.ReadModel.Models;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerBrewedEventHandler : IDomainEventHandlerAsync<BeerBrewedEvent>
{
    private readonly IBeerService _beerService;
    private readonly ILogger _logger;

    public BeerBrewedEventHandler(ILoggerFactory loggerFactory, IBeerService beerService)
    {
        _beerService = beerService;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task HandleAsync(BeerBrewedEvent @event, CancellationToken cancellationToken = new ())
    {
        try
        {
            await _beerService.CreateBeerAsync(new BeerId(@event.AggregateId.Value), @event.Quantity, "",
                new Ingredients());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}