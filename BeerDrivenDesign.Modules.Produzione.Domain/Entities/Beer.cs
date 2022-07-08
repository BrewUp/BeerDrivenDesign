using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using BeerDrivenDesign.Modules.Produzione.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerId _beerId;
    private BeerType _beerType;
    private Quantity _quantity;
    private HopQuantity _hopQuantity;

    protected Beer()
    {
    }

    private Beer(BeerId beerId, Quantity quantity)
    {
        RaiseEvent(new BeerBrewedEvent(beerId, quantity));
    }

    private void Apply(BeerBrewedEvent @event)
    {
        Id = @event.AggregateId;
        _beerId = new BeerId(@event.AggregateId.Value);
        _quantity = @event.Quantity;
    }

    internal static Beer CreateBeer(BeerId beerId, Quantity quantity)
    {
        return new Beer(beerId, quantity);
    }
}