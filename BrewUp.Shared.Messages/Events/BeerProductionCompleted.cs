using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerProductionCompleted : DomainEvent
{
    public BeerId BeerId { get; }
    public BatchId BatchId { get; }
    public Quantity Quantity { get; }
    public ProductionCompleteTime ProductionCompleteTime { get; }

    public BeerProductionCompleted(BeerId beerId, BatchId batchId, Quantity quantity, ProductionCompleteTime productionCompleteTime) : base(beerId)
    {
        BeerId = beerId;
        BatchId = batchId;
        Quantity = quantity;
        ProductionCompleteTime = productionCompleteTime;
    }
}