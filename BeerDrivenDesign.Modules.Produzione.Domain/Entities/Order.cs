using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Enums;
using BrewUp.Shared.Messages.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class Order : AggregateRoot
{
    private BatchNumber _batchNumber;

    private BeerId _beerId;
    private BeerType _beerType;

    private Quantity _quantityToBeProduced = new(0);
    private Quantity _quantityProduced = new(0);
    private Quantity _quantityAvailable = new(0);

    private ProductionStartTime _productionStartTime;
    private ProductionCompleteTime _productionCompleteTime;

    private OrderStatus _orderStatus;

    protected Order()
    {}

    #region StartProduction
    public static Order StartBeerProduction(BatchId batchId, BatchNumber batchNumber, BeerId beerId, BeerType beerType,
        Quantity quantity, ProductionStartTime productionStartTime)
    {
        return new Order(batchId, batchNumber, beerId, beerType, quantity, productionStartTime);
    }

    private Order(BatchId batchId, BatchNumber batchNumber, BeerId beerId, BeerType beerType, Quantity quantity,
        ProductionStartTime productionStartTime)
    {
        RaiseEvent(new BeerProductionStarted(batchId, batchNumber, beerId, beerType, quantity, productionStartTime));
    }

    private void Apply(BeerProductionStarted @event)
    {
        Id = @event.AggregateId;
        _batchNumber = @event.BatchNumber;

        _beerId = @event.BeerId;
        _beerType = @event.BeerType;

        _productionStartTime = @event.ProductionStartTime;
        _quantityToBeProduced = @event.Quantity;

        _productionCompleteTime = new ProductionCompleteTime(DateTime.MinValue);
        _quantityProduced = new Quantity(0);

        _orderStatus = OrderStatus.Open;
    }
    #endregion

    #region CompleteProduction
    internal void CompleteBeerProduction(Quantity quantity, ProductionCompleteTime productionCompleteTime)
    {
        if (_orderStatus.Equals(OrderStatus.Completed))
            RaiseEvent(new ProductionExceptionHappened(new BatchId(Id.Value),
                $"Order {_batchNumber.Value} already completed!"));
        else
            RaiseEvent(new BeerProductionCompleted(new BatchId(Id.Value), _batchNumber, _beerId, quantity,
                productionCompleteTime));
    }

    private void Apply(BeerProductionCompleted @event)
    {
        _quantityProduced = @event.Quantity;
        _quantityAvailable = @event.Quantity;
        _productionCompleteTime = @event.ProductionCompleteTime;

        _orderStatus = OrderStatus.Completed;
    }
    #endregion

    #region Bottling
    internal void BottlingBeer(BottleHalfLitre bottleHalfLitre)
    {
        if (_quantityAvailable.Value - bottleHalfLitre.Value * 0.5 >= 0)
        {
            RaiseEvent(new BeerBottledV2(new BatchId(Id.Value), bottleHalfLitre,
                new Quantity(_quantityAvailable.Value - bottleHalfLitre.Value * 0.5), new BeerLabel("Label")));
        }
        else
        {
            RaiseEvent(new ProductionExceptionHappened(new BatchId(Id.Value), "Non hai abbastanza birra!!!!"));
        }
    }

    private void Apply(BeerBottled @event)
    {
        _quantityAvailable = @event.Quantity;
    }

    private void Apply(BeerBottledV2 @event)
    {
        _quantityAvailable = @event.Quantity;
    }
    #endregion

    #region Exceptions
    private void Apply(ProductionExceptionHappened @eventExceptionHappened)
    { }
    #endregion
}