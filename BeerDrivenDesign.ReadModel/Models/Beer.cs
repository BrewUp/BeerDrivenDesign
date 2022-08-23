using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BeerDrivenDesign.ReadModel.Abstracts;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.ReadModel.Models;

public class Beer : ModelBase
{
    public string BeerType { get; private set; } = string.Empty;
    public double Quantity { get; private set; } = 0;

    protected Beer()
    {}

    public static Beer CreateBeer(BeerId beerId, BeerType beerType, BatchId batchId,
        ProductionStartTime productionStartTime) =>
        new(beerId.Value, beerType.Value);

    private Beer(Guid beerId, string beerType)
    {
        Id = beerId.ToString();
        BeerType = beerType;
    }

    public BeerJson ToJson() => new()
    {
        BeerId = Id,
        BeerType = BeerType,
        Quantity = Quantity
    };
}