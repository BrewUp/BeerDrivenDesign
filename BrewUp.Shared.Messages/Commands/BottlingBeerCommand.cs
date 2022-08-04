using BrewUp.Shared.Messages.CustomTypes;

namespace BrewUp.Shared.Messages.Commands;

public sealed class BottlingBeerCommand : MassCommand
{
    public readonly BeerId BeerId;
    public readonly BottleHalfLitre BottleHalfLitre;

    public BottlingBeerCommand(Guid aggregateId, BottleHalfLitre bottleHalfLitre) : base(aggregateId)
    {
        BeerId = new BeerId(aggregateId);
        BottleHalfLitre = bottleHalfLitre;
    }
}