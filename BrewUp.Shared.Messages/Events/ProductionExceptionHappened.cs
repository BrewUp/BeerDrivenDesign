using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class ProductionExceptionHappened : DomainEvent
{
    public readonly string Message;

    public ProductionExceptionHappened(BatchId aggregateId, string message) : base(aggregateId)
    {
        Message = message;
    }
}