using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public class BeerTypesSetEvent : DomainEvent
{
    public BeerType BeerType { get; }

    public BeerTypesSetEvent(BeerId aggregateId, BeerType beerType) :
        base(aggregateId)
    {
        BeerType = beerType;
    }
}