using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task CreateBeerAsync(BeerId beerId, Quantity quantity, BeerType beerType, BatchId batchId, ProductionStartTime productionStartTime);
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}