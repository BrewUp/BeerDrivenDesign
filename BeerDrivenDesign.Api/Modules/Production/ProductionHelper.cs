using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.Api.Modules.Production.Concretes;
using BeerDrivenDesign.Api.Modules.Production.Validators;
using BeerDrivenDesign.Api.Shared.Concretes;
using FluentValidation.AspNetCore;

namespace BeerDrivenDesign.Api.Modules.Production;

public static class ProductionHelper
{
    public static IServiceCollection AddProductionModule(this IServiceCollection services)
    {
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<ProductionBeerValidator>());

        services.AddScoped<IProductionOrchestrator, ProductionOrchestrator>();

        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IBeerService, BeerService>();

        return services;
    }
}