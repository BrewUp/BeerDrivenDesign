﻿using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Shared.DTO;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Models;
using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class BeerService : ProductionBaseService, IBeerService
{
    private readonly IPersister _persister;

    public BeerService(IPersister persister,
        ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        _persister = persister;
    }

    public async Task CreateBeerAsync(BeerId beerId, Quantity quantity, string beerType, Ingredients ingredients)
    {
        try
        {
            var beer = Beer.CreateBeer(beerId, quantity, beerType, ingredients);
            await _persister.InsertAsync(beer);
        }
        catch (Exception ex)
        {
            Logger.LogError($"An error occured message {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<BeerJson>> GetBeersAsync()
    {
        try
        {
            var beers = await _persister.FindAsync<Beer>();
            var beersArray = beers as Beer[] ?? beers.ToArray();

            return beersArray.Any()
                ? beersArray.Select(b => b.ToJson())
                : Enumerable.Empty<BeerJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError($"An error occured message {ex.Message}");
            throw;
        }
    }
}