namespace BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

public class ProductionOrderJson
{
    public string BatchId { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;

    public string BeerId { get; set; } = string.Empty;
    public string BeerType { get; set; } = string.Empty;

    public DateTime ProductionStartTime { get; set; } = DateTime.MinValue;
    public DateTime ProductionCompleteTime { get; set; } = DateTime.MinValue;

    public string Status { get; set; } = string.Empty;
}