using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events
{
    public class BeerProductionStarted : DomainEvent
    {
        public BeerId BeerId { get; }
        public BatchId BatchId { get; }
        public Quantity Quantity { get; }
        public ProductionStartTime ProductionStartTime { get; }

        public BeerProductionStarted(BeerId beerId, BatchId batchId, Quantity quantity, ProductionStartTime productionStartTime) : base(beerId)
        {
            BeerId = beerId;
            BatchId = batchId;
            Quantity = quantity;
            ProductionStartTime = productionStartTime;
        }
    }
}
