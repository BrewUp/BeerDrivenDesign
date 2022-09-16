using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Endpoints;
using BeerDrivenDesign.Modules.Produzione.Shared.Validators;
using FluentValidation.AspNetCore;

namespace BeerDrivenDesign.Api.Modules;

public class ProductionModule
{
    public static IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ValidationHandler>();
        builder.Services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<ProductionBeerValidator>());

        builder.Services.AddScoped<IProductionOrchestrator, ProductionOrchestrator>();

        builder.Services.AddScoped<IProductionService, ProductionService>();
        builder.Services.AddScoped<IBeerService, BeerService>();

        return builder.Services;
    }

    public static IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("v1/production/beers/brew", ProductionEndpoints.HandleStartProduction)
            .WithTags("Production");
        endpoints.MapPut("v1/production/beers/brew/{productionNumber}", ProductionEndpoints.HandleCompleteProduction)
            .WithTags("Production");

        endpoints.MapGet("v1/production/beers", ProductionEndpoints.HandleGetBeers)
            .WithTags("Production");
        endpoints.MapGet("v1/production", ProductionEndpoints.HandleGetProductionOrders)
            .WithTags("Production");

        return endpoints;
    }
}