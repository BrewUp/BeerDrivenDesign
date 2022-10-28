namespace BeerDrivenDesign.Modules.Purchases.CustomTypes;

public record PurchaseOrderEntity(OrderId OrderId, OrderNumber OrderNumber, FornitoreId FornitoreId, IEnumerable<OrderRow> Rows);