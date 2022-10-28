namespace BeerDrivenDesign.Modules.Purchases.Dtos;

public class PurchaseOrderRowJson
{
    public string OrderId { get; set; } = string.Empty;
    public string RowId { get; set; } = string.Empty;
    public string CodiceArticolo { get; set; } = string.Empty;
    public double Quantita { get; set; } = 0;
    public double CostoUnitario { get; set; } = 0;
}