using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.ReadModel.Models;

public class Beer : ModelBase
{
    public string BeerType { get; private set; } = string.Empty;
    public Ingredients Ingredients { get; private set; } = new();

    protected Beer()
    {}

    public static Beer CreateBeer(Guid beerId, string beerType, Ingredients ingredients) =>
        new(beerId, beerType, ingredients);

    private Beer(Guid beerId, string beerType, Ingredients ingredients)
    {
        Id = beerId.ToString();
        BeerType = beerType;
        Ingredients = ingredients;
    }
}