using BrewUp.Shared.Messages.CustomTypes;

namespace BrewUp.Shared.Messages.Commands;

public class CompleteBeerProductionCommand : MassCommand
{
    public CompleteBeerProductionCommand(BatchId batchId, BeerId beerId, Quantity quantity, ProductionCompleteTime productionCompleteTime)
        : base(beerId.Value)
    {
        BatchId = batchId;
        BeerId = beerId;
        ProductionCompleteTime = productionCompleteTime;
        Quantity = quantity;
    }

    public BatchId BatchId { get; }

    public BeerId BeerId { get; }
    public Quantity Quantity { get; }

    public ProductionCompleteTime ProductionCompleteTime { get; }
}