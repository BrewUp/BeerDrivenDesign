using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionService
{
    Task CreateProductionOrderAsync(BatchId batchId, BatchNumber batchNumber, BeerId beerId, BeerType beerType,
        Quantity quantity, ProductionStartTime productionStartTime);
    Task CompleteProductionOrderAsync(BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime);

    Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync();
}