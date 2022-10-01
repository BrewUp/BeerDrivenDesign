using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class BottlingBeer : Command
{
    public readonly BatchId BatchId;
    public readonly BottleHalfLitre BottleHalfLitre;

    public BottlingBeer(BatchId aggregateId, BottleHalfLitre bottleHalfLitre) : base(aggregateId)
    {
        BatchId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
    }
}