﻿using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IBeerService _beerService;

    public BeerProductionStartedEventHandler(ILoggerFactory loggerFactory, IBeerService beerService) : base(loggerFactory)
    {
        _beerService = beerService;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _beerService.CreateBeerAsync(@event.BeerId, @event.BeerType, @event.BatchId,
                @event.ProductionStartTime);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}