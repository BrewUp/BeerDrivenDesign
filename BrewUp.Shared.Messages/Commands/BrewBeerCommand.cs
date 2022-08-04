using System.Text.Json.Serialization;
using BrewUp.Shared.Messages.CustomTypes;

namespace BrewUp.Shared.Messages.Commands;

public class BrewBeerCommand : MassCommand
{
    [JsonPropertyName("quantity")]
    public Quantity Quantity { get; }
    [JsonPropertyName("beerType")]
    public BeerType BeerType { get; }
    [JsonPropertyName("hopQuantity")]
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