using BeerDrivenDesign.Modules.Produzione;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.DTO;
using BeerDrivenDesign.ReadModel;
using FluentValidation;

namespace BeerDrivenDesign.Api.Modules;

public class ProductionModule : IModule
{
    private const string BaseEndpointUrl = "/production";

    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProduzione();
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{BaseEndpointUrl}/beer/brew", HandleBrewBeer);

        return endpoints;
    }

    private static async Task<IResult> HandleBrewBeer(
        IProductionService productionService,
        IValidator<BrewBeer> validator,
        ValidationHandler validationHandler,
        BrewBeer body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        productionService.Brew(body);

        return Results.Accepted();
    }
}