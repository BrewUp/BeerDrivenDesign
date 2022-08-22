using BeerDrivenDesign.Modules.Produzione.Shared.DTO;
using BeerDrivenDesign.ReadModel.Abstracts;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.ReadModel.Models;

public class Beer : ModelBase
{
    public string BeerType { get; private set; } = string.Empty;
    public double Quantity { get; private set; } = 0;
    public Ingredients Ingredients { get; private set; } = new();

    protected Beer()
    {}

    public static Beer CreateBeer(BeerId beerId, Quantity quantity, string beerType, Ingredients ingredients) =>
        new(beerId.Value, quantity.Value, beerType, ingredients);

    private Beer(Guid beerId, double quantity, string beerType, Ingredients ingredients)
    {
        Id = beerId.ToString();
        Quantity = quantity;
        BeerType = beerType;
        Ingredients = ingredients;
    }

    public BeerJson ToJson() => new()
    {
        BeerId = Id,
        BeerType = BeerType,
        Quantity = Quantity
    };
}