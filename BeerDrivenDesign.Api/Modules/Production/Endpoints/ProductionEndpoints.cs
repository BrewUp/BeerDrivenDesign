using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.ReadModel.Dtos;
using FluentValidation;

namespace BeerDrivenDesign.Api.Modules.Production.Endpoints;

public static class ProductionEndpoints
{
    public static async Task<IResult> HandleStartProduction(
        IProductionOrchestrator productionService,
        IValidator<PostProductionBeer> validator,
        ValidationHandler validationHandler,
        PostProductionBeer body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        await productionService.StartProductionAsync(body);

        return Results.Accepted();
    }

    public static async Task<IResult> HandleCompleteProduction(
        IProductionOrchestrator productionService,
        string batchNumber,
        IValidator<PostProductionBeer> validator,
        ValidationHandler validationHandler,
        PostProductionBeer body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        await productionService.CompleteProductionAsync(body);

        return Results.Accepted();
    }

    public static async Task<IResult> HandleGetBeers(IBeerService beerService)
    {
        var beers = await beerService.GetBeersAsync();

        return Results.Ok(beers);
    }

    public static async Task<IResult> HandleGetProductionOrders(IProductionService productionService)
    {
        var orders = await productionService.GetProductionOrdersAsync();

        return Results.Ok(orders);
    }

    public static async Task<IResult> HandleCreateRecipe(IProductionService productionService, RecipesJson recipe)
    {
        try
        {
            await productionService.CreateRecipesAsync(recipe);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}