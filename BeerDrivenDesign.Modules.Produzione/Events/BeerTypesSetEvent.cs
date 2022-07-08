using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Events;

public class BeerTypesSetEvent : DomainEvent
{
    public BeerType BeerType { get; }

    public BeerTypesSetEvent(BeerId aggregateId, BeerType beerType) :
        base(aggregateId)
    {
        BeerType = beerType;
    }
}