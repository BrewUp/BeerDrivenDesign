using BeerDrivenDesign.Modules.Purchases.Dtos;

namespace BeerDrivenDesign.Modules.Purchases.Abstracts;

public interface IPurchasesOrchstrator
{
    Task<string> AddPurchaseOrderAsync(PurchaseOrderJson orderToAdd);
}