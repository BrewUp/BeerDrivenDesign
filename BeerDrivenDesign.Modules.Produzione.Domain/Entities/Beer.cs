using System.ComponentModel;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerId _beerId = new (Guid.Empty);
    private BeerType _beerType = new("");
    private Quantity _quantityToBeProduced = new(0);
    private HopQuantity _hopQuantity = new(0);

    private BottleHalfLitre _bottleHalfLitre;
    private BatchId _batchId;

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
        _quantityToBeProduced = @event.Quantity;
    }

    internal static Beer CreateBeer(BeerId beerId, Quantity quantity)
    {
        return new Beer(beerId, quantity);
    }

    internal void BottlingBeer(BeerId beerId, BottleHalfLitre bottleHalfLitre)
    {
        if (_quantityToBeProduced.Value - (bottleHalfLitre.Value * 0.5) >= 0)
        {
            RaiseEvent(new BeerBottledV2(beerId, bottleHalfLitre,
                new Quantity(_quantityToBeProduced.Value - bottleHalfLitre.Value * 0.5), new BeerLabel("Label")));
        }

        RaiseEvent(new ProductionExceptionHappened(beerId, "Non hai abbastanza birra!!!!"));
    }

    private void Apply(BeerBottled @event)
    {
        _quantityToBeProduced = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }

    private void Apply(BeerBottledV2 @event)
    {
        _quantityToBeProduced = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }

    public static Beer StartBeerProduction(BeerId beerId, BatchId batchId, Quantity quantity)
    {
        /* - la validità del comando la controlla l'aggregato se è roba semplice, oppure un DomainService apposito
         *
         * Ad es. qui potremmo controllare che il lotto sia valido (un lotto recente, un lotto non già prodotto, etc.).
         * Oppure potremmo sapere/decidere che i dati ci arrivano già validati.
         * 
         * Dovremmo poi verificare le eventuali regole di business, ma qui non abbiamo nulla da verificare.
         *
         * Siccome questo evento è un "evento zero", il primo evento in assoluto della storia,
         * il suo scopo sarà costruire l'aggregato.
         *
         * Nel costruttore (privato), l'aggregato solleverà l'evento di dominio che ci interessa (BeerProductionStarted).
         *
         * L'Apply aplica l'evento, e inizializza/aggiorna le proprietà dell'aggregato.
         *
         * 
         *
         */

        return new Beer(beerId, batchId, quantity);
    }

    private Beer(BeerId beerId, BatchId batchId, Quantity quantity)
    {
        RaiseEvent(new BeerProductionStarted(beerId, batchId, quantity));
    }

    private void Apply(BeerProductionStarted @event)
    {
        Id = @event.AggregateId;
        _beerId = @event.BeerId;
        _batchId = @event.BatchId;
        _quantityToBeProduced = @event.Quantity;
    }

    private void Apply(ProductionExceptionHappened @eventExceptionHappened)
    { }
}