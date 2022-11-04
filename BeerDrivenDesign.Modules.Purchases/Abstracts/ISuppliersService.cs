using BeerDrivenDesign.Modules.Purchases.CustomTypes;

namespace BeerDrivenDesign.Modules.Purchases.Abstracts;

public interface ISuppliersService
{
    Task<bool> VerifySupplierAsync(FornitoreId fornitoreId);
}