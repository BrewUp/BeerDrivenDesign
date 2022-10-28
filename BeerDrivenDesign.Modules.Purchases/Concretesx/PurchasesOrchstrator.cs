using BeerDrivenDesign.Modules.Purchases.Abstracts;
using BeerDrivenDesign.Modules.Purchases.CustomTypes;
using BeerDrivenDesign.Modules.Purchases.Dtos;

namespace BeerDrivenDesign.Modules.Purchases.Concretesx;

public sealed class PurchasesOrchstrator : IPurchasesOrchstrator
{
    private readonly IPurchasesService _purchasesService;
    private readonly ISpareService _spareService;

    public PurchasesOrchstrator(IPurchasesService purchasesService, ISpareService serviceService)
    {
        _purchasesService = purchasesService;
        _spareService = serviceService;
    }

    public async Task<string> AddPurchaseOrderAsync(PurchaseOrderJson orderToAdd)
    {
        foreach (var spare in orderToAdd.Rows)
        {
            _spareService.VerifySpare(spare.CodiceArticolo);
        }

        await _purchasesService.AddPurchaseOrderAsync(MapToEntity(orderToAdd));

        return orderToAdd.OrderId;
    }

    public static PurchaseOrderEntity MapToEntity(PurchaseOrderJson json)
    {
        return new PurchaseOrderEntity(new OrderId(json.OrderId), new OrderNumber(json.OrderNumber),
            new FornitoreId(json.FornitoreId),
            json.Rows.Select(r => new OrderRow(new RowId(r.RowId),
                new Ingredient(new IngredientId(r.CodiceArticolo), new IngredientName("")), new Quantity(r.Quantita))));
    }
}