using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class StartBeerProduction : Command
{
    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly BeerId BeerId;
    public readonly BeerType BeerType;

    public readonly Quantity Quantity;
    public readonly ProductionStartTime ProductionStartTime;

    public StartBeerProduction(BatchId aggregateId, BatchNumber batchNumber, BeerId beerId, BeerType beerType,
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