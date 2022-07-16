using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Events;

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