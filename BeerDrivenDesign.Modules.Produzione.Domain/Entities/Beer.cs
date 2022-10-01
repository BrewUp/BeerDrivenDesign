using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : Entity
{
    private BeerId _beerId = new (Guid.Empty);
    private BeerType _beerType = new("");
    
    private Quantity _quantityAvailable = new(0);

    private BottleHalfLitre _bottleHalfLitre = new(0);

    protected Beer()
    {
    }
}