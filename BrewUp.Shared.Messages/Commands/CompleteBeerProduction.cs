using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public class CompleteBeerProduction : Command
{
    public readonly BatchId BatchId;
    public readonly BeerId BeerId;
    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public CompleteBeerProduction(BatchId batchId, BeerId beerId, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(beerId)
    {
        BatchId = batchId;
        BeerId = beerId;
        ProductionCompleteTime = productionCompleteTime;
        Quantity = quantity;
    }
}