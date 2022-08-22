using Muflone.Core;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class ProductionExceptionHappened : DomainEvent
{
    public readonly string Message;

    public ProductionExceptionHappened(IDomainId aggregateId, string message) : base(aggregateId)
    {
        Message = message;
    }
}