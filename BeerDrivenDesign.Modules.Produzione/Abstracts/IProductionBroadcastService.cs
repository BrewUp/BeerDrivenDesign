using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionBroadcastService
{
    Task PublishProductionOrderUpdatedAsync(BatchId batchId);
}