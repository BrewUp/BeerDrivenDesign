namespace BeerDrivenDesign.Modules.Purchases.Dtos;

public class PurchaseOrderJson
{
    public string OrderId { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string FornitoreId { get; set; } = string.Empty;
    public DateTime DataInserimento { get; set; } = DateTime.MinValue;
    public DateTime DataPrevistaConsegna { get; set; } = DateTime.MinValue;

    public IEnumerable<PurchaseOrderRowJson> Rows { get; set; } = Enumerable.Empty<PurchaseOrderRowJson>();
}