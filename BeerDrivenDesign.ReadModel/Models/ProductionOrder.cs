using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.ReadModel.Models;

public class ProductionOrder : ModelBase
{
    public string BatchId { get; private set; } = string.Empty;

    public string BeerId { get; private set; } = string.Empty;
    public string BeerType { get; private set; } = string.Empty;
    public double QuantityToProduce { get; private set; } = 0;
    public double QuantityProduced { get; private set; } = 0;
    public DateTime ProductionStartTime { get; private set; } = DateTime.MinValue;
    public DateTime ProductionCompleteTime { get; private set; } = DateTime.MinValue;
    public string Status { get; private set; } = string.Empty;

    protected ProductionOrder()
    {}

    private ProductionOrder(Guid batchId, string batchNumber, string beerId, string beerType, double quantity, DateTime productionStartTime)
    {
        Id = batchNumber;
        BatchId = batchId.ToString();
        
        BeerId = beerId;
        BeerType = beerType;

        QuantityToProduce = quantity;
        QuantityProduced = 0;

        ProductionStartTime = productionStartTime;
        Status = "Open";
    }

    public ProductionOrderJson ToJson() => new()
    {
        BatchId = BatchId,
        BatchNumber = Id,

        BeerId = BeerId,
        BeerType = BeerType,

        QuantityToProduce = QuantityToProduce,
        QuantityProduced = QuantityProduced,

        ProductionStartTime = ProductionStartTime,
        ProductionCompleteTime = ProductionCompleteTime,

        Status = Status
    };
}