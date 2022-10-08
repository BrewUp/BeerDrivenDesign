using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;
using BeerDrivenDesign.ReadModel.Models;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class BeerService : ProductionBaseService, IBeerService
{
    public BeerService(IPersister persister,
        ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
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