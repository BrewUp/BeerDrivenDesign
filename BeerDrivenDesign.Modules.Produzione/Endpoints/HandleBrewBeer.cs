﻿using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BeerDrivenDesign.Modules.Produzione.Endpoints;

public static class ProductionEndpoints
{
    public static async Task<IResult> HandleBrewBeer(
        IProductionService productionService,
        IValidator<PostBrewBeer> validator,
        ValidationHandler validationHandler,
        PostBrewBeer body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        await productionService.Brew(body);

        return Results.Accepted();
    }

    public static async Task<IResult> HandleGetBeers(IBeerService beerService)
    {
        var beers = await beerService.GetBeersAsync();

        return Results.Ok(beers);
    }
}