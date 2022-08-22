using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerId _beerId = new (Guid.Empty);
    private BeerType _beerType = new("");
    private Quantity _quantityToBeProduced = new(0);
    private Quantity _quantityProduced = new(0);
    private HopQuantity _hopQuantity = new(0);

    private BottleHalfLitre _bottleHalfLitre;
    private BatchId _batchId;
    private ProductionStartTime _productionStartTime;
    private ProductionCompleteTime _productionCompleteTime;

    protected Beer()
    {
    }

    #region Bottling
    internal void BottlingBeer(BeerId beerId, BottleHalfLitre bottleHalfLitre)
    {
        if (_quantityProduced.Value - (bottleHalfLitre.Value * 0.5) >= 0)
        {
            RaiseEvent(new BeerBottledV2(beerId, bottleHalfLitre,
                new Quantity(_quantityProduced.Value - bottleHalfLitre.Value * 0.5), new BeerLabel("Label")));
        }
        else
        {
            RaiseEvent(new ProductionExceptionHappened(beerId, "Non hai abbastanza birra!!!!"));
        }
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
    #endregion

    #region StartProduction
    public static Beer StartBeerProduction(BeerId beerId, BeerType beerType, BatchId batchId, Quantity quantity,
        ProductionStartTime productionStartTime)
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
         * L'Apply applica l'evento, e inizializza/aggiorna le proprietà dell'aggregato.
         */

        return new Beer(beerId, beerType, batchId, quantity, productionStartTime);
    }

    private Beer(BeerId beerId, BeerType beerType, BatchId batchId, Quantity quantity,
        ProductionStartTime productionStartTime)
    {
        RaiseEvent(new BeerProductionStarted(beerId, beerType, batchId, quantity, productionStartTime));
    }

    private void Apply(BeerProductionStarted @event)
    {
        Id = @event.AggregateId;
        _beerId = @event.BeerId;
        _beerType = @event.BeerType;
        _batchId = @event.BatchId;
        _quantityToBeProduced = @event.Quantity;
        _productionStartTime = @event.ProductionStartTime;
    }
    #endregion

    #region CompleteProduction
    internal void CompleteBeerProduction(BatchId batchId, Quantity quantity, ProductionCompleteTime productionCompleteTime)
    {
        RaiseEvent(new BeerProductionCompleted(_beerId, batchId, quantity, productionCompleteTime));
    }
    private void Apply(BeerProductionCompleted @event)
    {
        Id = @event.AggregateId;
        _beerId = @event.BeerId;
        _batchId = @event.BatchId;
        _quantityProduced = @event.Quantity;
        _productionCompleteTime = @event.ProductionCompleteTime;
    }
    #endregion

    #region Exceptions
    private void Apply(ProductionExceptionHappened @eventExceptionHappened)
    { }
    #endregion
}