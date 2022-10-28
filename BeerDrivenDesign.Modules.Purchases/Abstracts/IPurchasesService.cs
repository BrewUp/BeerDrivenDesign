using BeerDrivenDesign.Modules.Purchases.CustomTypes;

namespace BeerDrivenDesign.Modules.Purchases.Abstracts;

public interface IPurchasesService
{
    Task<string> AddPurchaseOrderAsync(PurchaseOrderEntity orderToAdd);
}