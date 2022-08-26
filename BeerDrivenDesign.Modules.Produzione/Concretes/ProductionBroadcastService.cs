using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProductionBroadcastService : IProductionBroadcastService
{
    private readonly ProductionHub _productionHub;

    public ProductionBroadcastService(ProductionHub productionHub)
    {
        _productionHub = productionHub;
    }

    public async Task PublishProductionOrderUpdatedAsync(BatchId batchId)
    {
        await _productionHub.ProductionOrderStartedUpdatedAsync(batchId);
    }
}