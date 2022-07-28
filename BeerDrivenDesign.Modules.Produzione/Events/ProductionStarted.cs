using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Events;

public sealed class ProductionStarted : IntegrationEvent
{
    public readonly BeerId BeerId;
    public readonly ProductionStartTime ProductionStartTime;
    public readonly Quantity Quantity;
    public readonly BatchId BatchId;

    public ProductionStarted(BeerId aggregateId, ProductionStartTime productionStartTime, Quantity quantity, BatchId batchId) : base(aggregateId)
    {
        BeerId = aggregateId;
        ProductionStartTime = productionStartTime;
        Quantity = quantity;
        BatchId = batchId;
    }
}