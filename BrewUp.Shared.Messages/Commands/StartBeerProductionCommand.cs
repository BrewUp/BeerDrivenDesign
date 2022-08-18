using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class StartBeerProductionCommand : Command
{
    public StartBeerProductionCommand(BatchId batchId, BeerId beerId, Quantity quantity, ProductionStartTime productionStartTime) : base(beerId)
    {
        BatchId = batchId;
        BeerId = beerId;
        Quantity = quantity;
        ProductionStartTime = productionStartTime;
    }

    public BatchId BatchId { get; }

    public BeerId BeerId { get; }

    public Quantity Quantity { get; }

    public ProductionStartTime ProductionStartTime { get; }
}