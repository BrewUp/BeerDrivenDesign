﻿using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerProductionCompleted : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BatchId BatchId;
    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public BeerProductionCompleted(BeerId beerId, BatchId batchId, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(beerId)
    {
        BeerId = beerId;
        BatchId = batchId;
        Quantity = quantity;
        ProductionCompleteTime = productionCompleteTime;
    }
}