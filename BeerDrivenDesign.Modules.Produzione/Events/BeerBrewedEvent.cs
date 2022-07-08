using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Events;

public class BeerBrewedEvent : DomainEvent
{
    public Quantity Quantity { get; }

    public BeerBrewedEvent(BeerId aggregateId, Quantity quantity) :
        base(aggregateId)
    {
        Quantity = quantity;
    }
}