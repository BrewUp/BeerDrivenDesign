using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Events;

public class ProductionExceptionHappened : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly string Message;

    public ProductionExceptionHappened(BeerId aggregateId, string message) : base(aggregateId)
    {
        BeerId = aggregateId;
        Message = message;
    }
}