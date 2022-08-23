using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public class CompleteBeerProduction : Command
{
    public readonly BeerId BeerId;

    public readonly BatchNumber BatchNumber;

    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public CompleteBeerProduction(BeerId aggregateId, BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(aggregateId)
    {
        BeerId = aggregateId;

        BatchNumber = batchNumber;

        ProductionCompleteTime = productionCompleteTime;
        Quantity = quantity;
    }
}