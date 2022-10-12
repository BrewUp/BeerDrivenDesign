using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;
using BeerDrivenDesign.ReadModel.Models;

namespace BeerDrivenDesign.Api.Modules.Production.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory, IPersister persister)
        : base(persister, loggerFactory)
    {
    }
    public async Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync()
    {
        try
        {
            var productionOrders = await Persister.FindAsync<ProductionOrder>();
            var ordersArray = productionOrders as ProductionOrder[] ??
                              productionOrders.OrderByDescending(p => p.Id).ToArray();

            return ordersArray.Any()
                ? ordersArray.Select(p => p.ToJson())
                : Enumerable.Empty<ProductionOrderJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}