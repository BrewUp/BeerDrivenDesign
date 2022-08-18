using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public class CompleteBeerProductionCommand : Command
{
    public CompleteBeerProductionCommand(BatchId batchId, BeerId beerId, Quantity quantity, ProductionCompleteTime productionCompleteTime)
        : base(beerId)
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