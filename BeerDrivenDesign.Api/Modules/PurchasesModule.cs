using BeerDrivenDesign.Modules.Purchases;
using BeerDrivenDesign.Modules.Purchases.Abstracts;
using BeerDrivenDesign.Modules.Purchases.Dtos;

namespace BeerDrivenDesign.Api.Modules;

public class PurchasesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddPurchasesModule();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("v1/purchases/orders", HandlePostPurchaseOrder)
            .WithTags("Purchases");

        return endpoints;
    }

    private static async Task<IResult> HandlePostPurchaseOrder(IPurchasesOrchstrator purchasesOrchstrator,
        PurchaseOrderJson body)
    {
        var orderId = await purchasesOrchstrator.AddPurchaseOrderAsync(body);

        return Results.Ok(orderId);
    }
}