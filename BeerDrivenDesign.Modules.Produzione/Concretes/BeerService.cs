using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Models;
using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class BeerService : ProductionBaseService, IBeerService
{
    public BeerService(IPersister persister,
        ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }

    public async Task CreateBeerAsync(BeerId beerId, BeerType beerType, BatchId batchId,
        ProductionStartTime productionStartTime)
    {
        try
        {
            var beer = Beer.CreateBeer(beerId, beerType, batchId, productionStartTime);
            await Persister.InsertAsync(beer);
        }
        catch (Exception ex)
        {
            Logger.LogError($"An error occurred message {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<BeerJson>> GetBeersAsync()
    {
        try
        {
            var beers = await Persister.FindAsync<Beer>();
            var beersArray = beers as Beer[] ?? beers.ToArray();

            return beersArray.Any()
                ? beersArray.Select(b => b.ToJson())
                : Enumerable.Empty<BeerJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError($"An error occurred message {ex.Message}");
            throw;
        }
    }
}