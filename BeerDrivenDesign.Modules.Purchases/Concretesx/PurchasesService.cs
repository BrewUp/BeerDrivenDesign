using BeerDrivenDesign.Modules.Purchases.Abstracts;
using BeerDrivenDesign.Modules.Purchases.CustomTypes;

namespace BeerDrivenDesign.Modules.Purchases.Concretesx;

public class PurchasesService : IPurchasesService
{
    public Task<string> AddPurchaseOrderAsync(PurchaseOrderEntity orderToAdd)
    {
        return Task.FromResult("123");
    }
}