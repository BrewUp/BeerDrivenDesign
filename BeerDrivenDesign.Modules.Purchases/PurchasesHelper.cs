using BeerDrivenDesign.Modules.Purchases.Abstracts;
using BeerDrivenDesign.Modules.Purchases.Concretesx;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Purchases;

public static class PurchasesHelper
{
    public static IServiceCollection AddPurchasesModule(this IServiceCollection services)
    {
        services.AddScoped<IPurchasesOrchstrator, PurchasesOrchstrator>();
        services.AddScoped<ISpareService, SpareService>();

        services.AddScoped<IPurchasesService, PurchasesService>();

        return services;
    }
}