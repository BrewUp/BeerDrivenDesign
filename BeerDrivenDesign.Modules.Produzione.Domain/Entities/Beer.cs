using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerId _beerId = new (Guid.Empty);
    private BeerType _beerType = new("");
    
    private Quantity _quantityAvailable = new(0);

    private BottleHalfLitre _bottleHalfLitre = new(0);

    private IEnumerable<ProductionOrder> _productionOrders = Enumerable.Empty<ProductionOrder>();

    protected Beer()
    {
    }

    #region StartProduction
    public static Beer StartBeerProduction(BeerId beerId, BeerType beerType, BatchId batchId, BatchNumber batchNumber,
        Quantity quantity, ProductionStartTime productionStartTime)
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

        return new Beer(beerId, beerType, batchId, batchNumber, quantity, productionStartTime);
    }

    private Beer(BeerId beerId, BeerType beerType, BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime)
    {
        RaiseEvent(new BeerProductionStarted(beerId, beerType, batchId, batchNumber, quantity, productionStartTime));
    }

    private void Apply(BeerProductionStarted @event)
    {
        Id = @event.AggregateId;
        _beerId = @event.BeerId;
        _beerType = @event.BeerType;

        var productionOrder =
            ProductionOrder.StartProduction(@event.BatchId, @event.BatchNumber, @event.Quantity,
                @event.ProductionStartTime);
        _productionOrders = _productionOrders.Concat(new List<ProductionOrder>
        {
            productionOrder
        });
    }
    #endregion

    #region MyRegion
    internal void StartNewProductionOrder(BeerId beerId, BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime)
    {

    }
    #endregion

    #region CompleteProduction
    internal void CompleteBeerProduction(BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime)
    {
        RaiseEvent(new BeerProductionCompleted(_beerId, batchNumber, quantity, productionCompleteTime));
    }
    private void Apply(BeerProductionCompleted @event)
    {
        Id = @event.AggregateId;
        _beerId = @event.BeerId;
        _quantityAvailable = new Quantity(_quantityAvailable.Value + @event.Quantity.Value);

        var productionOrder = _productionOrders.FirstOrDefault(p => p.batchNumber.Equals(@event.BatchNumber));
        if (productionOrder == null) return;

        productionOrder.CompleteProduction(@event.Quantity, @event.ProductionCompleteTime);
        _productionOrders = _productionOrders.Where(p => !p.batchNumber.Equals(@event.BatchNumber))
            .Concat(new List<ProductionOrder>
            {
                productionOrder
            });
    }
    #endregion

    #region Bottling
    internal void BottlingBeer(BeerId beerId, BottleHalfLitre bottleHalfLitre)
    {
        if (_quantityAvailable.Value - (bottleHalfLitre.Value * 0.5) >= 0)
        {
            RaiseEvent(new BeerBottledV2(beerId, bottleHalfLitre,
                new Quantity(_quantityAvailable.Value - bottleHalfLitre.Value * 0.5), new BeerLabel("Label")));
        }
        else
        {
            RaiseEvent(new ProductionExceptionHappened(beerId, "Non hai abbastanza birra!!!!"));
        }
    }

    private void Apply(BeerBottled @event)
    {
        _quantityAvailable = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }

    private void Apply(BeerBottledV2 @event)
    {
        _quantityAvailable = @event.Quantity;
        _bottleHalfLitre = @event.BottleHalfLitre;
    }
    #endregion

    #region Exceptions
    private void Apply(ProductionExceptionHappened @eventExceptionHappened)
    { }
    #endregion
}