using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task CreateBeerAsync(BeerId beerId, BeerType beerType, BatchId batchId, ProductionStartTime productionStartTime);
    Task UpdateBeerQuantityAsync(BeerId beerId, Quantity quantity);

    Task<IEnumerable<BeerJson>> GetBeersAsync();
}