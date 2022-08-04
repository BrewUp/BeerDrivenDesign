using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Core;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class StartBeerProductionCommand : MassCommand
{
    public BatchId BatchId { get; }

    public BeerId BeerId { get; }

    public Quantity Quantity { get; }

    public StartBeerProductionCommand(BatchId batchId, BeerId beerId, Quantity quantity) : base(beerId.Value)
    {
        BatchId = batchId;
        BeerId = beerId;
        Quantity = quantity;
    }
}