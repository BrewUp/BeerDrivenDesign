using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Shared.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Produzione;

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