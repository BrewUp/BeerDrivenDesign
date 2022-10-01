using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public sealed class BeerBottled : DomainEvent
{
    public readonly BatchId BatchId;
    public readonly BottleHalfLitre BottleHalfLitre;
    public readonly Quantity Quantity;

    public BeerBottled(BatchId aggregateId, BottleHalfLitre bottleHalfLitre, Quantity quantity) : base(aggregateId)
    {
        BatchId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
        Quantity = quantity;
    }
}