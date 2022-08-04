using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public sealed class BeerBottled : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BottleHalfLitre BottleHalfLitre;
    public readonly Quantity Quantity;

    public BeerBottled(BeerId aggregateId, BottleHalfLitre bottleHalfLitre, Quantity quantity) : base(aggregateId)
    {
        BeerId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
        Quantity = quantity;
    }
}