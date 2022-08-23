using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class ProductionOrder : Entity
{
    private BatchNumber _batchNumber = new (string.Empty);
    private Quantity _quantityToBeProduced = new(0);
    private Quantity _quantityProduced = new(0);

    private ProductionStartTime _productionStartTime;
    private ProductionCompleteTime _productionCompleteTime;

    protected ProductionOrder()
    {}

    internal static ProductionOrder StartProduction(BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime) => new(batchId, batchNumber, quantity, productionStartTime);

    private ProductionOrder(BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime) : base(batchId)
    {
        _batchNumber = batchNumber;

        _quantityToBeProduced = quantity;
        _productionStartTime = productionStartTime;

        _productionCompleteTime = new ProductionCompleteTime(DateTime.MinValue);
    }

    internal BatchNumber batchNumber => _batchNumber;

    internal void CompleteProduction(Quantity quantity, ProductionCompleteTime productionCompleteTime)
    {
        _quantityProduced = quantity;
        _productionCompleteTime = productionCompleteTime;
    }
}