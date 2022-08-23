using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerProductionCompleted : DomainEvent
{
    public readonly BeerId BeerId;

    public readonly BatchNumber BatchNumber;

    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public BeerProductionCompleted(BeerId aggregateId, BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(aggregateId)
    {
        BeerId = aggregateId;

        BatchNumber = batchNumber;

        Quantity = quantity;
        ProductionCompleteTime = productionCompleteTime;
    }
}