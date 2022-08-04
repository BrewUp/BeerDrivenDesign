using System.Text.Json.Serialization;
using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Events;

public sealed class ProductionStarted : IntegrationEvent
{
    [JsonPropertyName("beerId")]
    public readonly BeerId BeerId;
    [JsonPropertyName("productionStartTime")]
    public readonly ProductionStartTime ProductionStartTime;
    [JsonPropertyName("quantity")]
    public readonly Quantity Quantity;
    [JsonPropertyName("batchId")]
    public readonly BatchId BatchId;

    public ProductionStarted(BeerId aggregateId, ProductionStartTime productionStartTime,
        Quantity quantity, BatchId batchId) : base(aggregateId)
    {
        BeerId = aggregateId;
        ProductionStartTime = productionStartTime;
        Quantity = quantity;
        BatchId = batchId;
    }
}