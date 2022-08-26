using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.CustomTypes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BeerDrivenDesign.Modules.Produzione.Endpoints;

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

    public static async Task<IResult> HandleGetSignalR(IProductionBroadcastService productionBroadcastService)
    {
        await productionBroadcastService.PublishProductionOrderUpdatedAsync(new BatchId(Guid.NewGuid()));

        return Results.Ok();
    }
}