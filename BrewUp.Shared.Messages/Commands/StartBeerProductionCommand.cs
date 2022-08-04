using BrewUp.Shared.Messages.CustomTypes;

namespace BrewUp.Shared.Messages.Commands;

public sealed class StartBeerProductionCommand : MassCommand
{
    public StartBeerProductionCommand(BatchId batchId, BeerId beerId, Quantity quantity, ProductionStartTime productionStartTime) : base(beerId.Value)
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