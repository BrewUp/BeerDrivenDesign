using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerBrewedEvent : DomainEvent
{
    public Quantity Quantity { get; }

    public BeerBrewedEvent(BeerId aggregateId, Quantity quantity) :
        base(aggregateId)
    {
        Quantity = quantity;
    }
}