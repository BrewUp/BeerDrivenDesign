using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Modules.Produzione.Commands;

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