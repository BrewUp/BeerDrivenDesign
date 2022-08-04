using System.Text.Json.Serialization;
using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public class BrewBeerCommand : Command
{
    [JsonPropertyName("quantity")]
    public Quantity Quantity { get; }
    [JsonPropertyName("beerType")]
    public BeerType BeerType { get; }
    [JsonPropertyName("hopQuantity")]
    public HopQuantity HopQuantity { get; }

    public BrewBeerCommand(BeerId aggregateId, Quantity quantity, BeerType beerType, HopQuantity hopQuantity) :
        base(aggregateId)
    {
        Quantity = quantity;
        BeerType = beerType;
        HopQuantity = hopQuantity;
    }
}