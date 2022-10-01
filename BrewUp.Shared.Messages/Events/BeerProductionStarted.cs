using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerProductionStarted : DomainEvent
{
    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly BeerId BeerId;
    public readonly BeerType BeerType;

    public readonly Quantity Quantity;
    public readonly ProductionStartTime ProductionStartTime;

    public BeerProductionStarted(BatchId aggregateId, BatchNumber batchNumber, BeerId beerId, BeerType beerType,
        Quantity quantity, ProductionStartTime productionStartTime) : base(aggregateId)
    {
        BatchId = aggregateId;
        BatchNumber = batchNumber;

        BeerId = beerId;
        BeerType = beerType;

        Quantity = quantity;
        ProductionStartTime = productionStartTime;
    }
}