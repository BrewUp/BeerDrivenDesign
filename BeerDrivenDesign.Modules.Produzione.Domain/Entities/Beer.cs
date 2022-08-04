using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerId _beerId = new (Guid.Empty);
    private BeerType _beerType = new("");
    private Quantity _quantity = new(0);
    private HopQuantity _hopQuantity = new(0);

    private BottleHalfLitre _bottleHalfLitre;

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

    internal void BottlingBeer(BeerId beerId, BottleHalfLitre bottleHalfLitre)
    {
        if (_quantity.Value - (bottleHalfLitre.Value * 0.5) >= 0)
        {
            RaiseEvent(new BeerBottledV2(beerId, bottleHalfLitre,
                new Quantity(_quantity.Value - bottleHalfLitre.Value * 0.5), new BeerLabel("Label")));
        }

        RaiseEvent(new ProductionExceptionHappened(beerId, "Non hai abbastanza birra!!!!"));
    }

    private void Apply(BeerBottled @event)
    {
        _quantity = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }

    private void Apply(BeerBottledV2 @event)
    {
        _quantity = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }

    private void Apply(ProductionExceptionHappened @eventExceptionHappened)
    {}
}