using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Modules.Produzione.Commands;

public class BrewBeerCommand : Command
{
    public Quantity Quantity { get; }
    public BeerType BeerType { get; }
    public HopQuantity HopQuantity { get; }

    public BrewBeerCommand(BeerId aggregateId, Quantity quantity, BeerType beerType, HopQuantity hopQuantity) :
        base(aggregateId)
    {
        Quantity = quantity;
        BeerType = beerType;
        HopQuantity = hopQuantity;
    }
}