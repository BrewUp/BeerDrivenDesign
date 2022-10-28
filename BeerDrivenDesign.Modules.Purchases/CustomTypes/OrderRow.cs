namespace BeerDrivenDesign.Modules.Purchases.CustomTypes;

public record OrderRow(RowId RowId, Ingredient Ingredient, Quantity Quantity);