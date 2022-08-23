using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class StartBeerProduction : Command
{
    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;
    public readonly BeerType BeerType;
    public readonly Quantity Quantity;
    public readonly ProductionStartTime ProductionStartTime;

    public StartBeerProduction(BeerId aggregateId, BatchId batchId, BatchNumber batchNumber, BeerType beerType,
        Quantity quantity, ProductionStartTime productionStartTime) : base(aggregateId)
    {
        BatchId = batchId;
        BatchNumber = batchNumber;

        BeerType = beerType;
        
        Quantity = quantity;
        ProductionStartTime = productionStartTime;
    }
}