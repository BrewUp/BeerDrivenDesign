using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public sealed class BottlingBeerCommand : Command
{
    public readonly BeerId BeerId;
    public readonly BottleHalfLitre BottleHalfLitre;

    public BottlingBeerCommand(BeerId aggregateId, BottleHalfLitre bottleHalfLitre) : base(aggregateId)
    {
        BeerId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
    }
}