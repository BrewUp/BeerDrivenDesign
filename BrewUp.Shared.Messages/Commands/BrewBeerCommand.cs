using System.Text.Json.Serialization;
using BrewUp.Shared.Messages.CustomTypes;

namespace BrewUp.Shared.Messages.Commands;

public class BrewBeerCommand : MassCommand
{
    public Quantity Quantity { get; }
    public BeerType BeerType { get; }
    public HopQuantity HopQuantity { get; }

    [JsonConstructor]
    protected BrewBeerCommand()
    {}

    public BrewBeerCommand(Guid aggregateId, Quantity quantity, BeerType beerType, HopQuantity hopQuantity) :
        base(aggregateId)
    {
        Quantity = quantity;
        BeerType = beerType;
        HopQuantity = hopQuantity;
    }
}