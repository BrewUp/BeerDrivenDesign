using BeerDrivenDesign.Api.Modules.Magazzino.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Magazzino.Endpoints;

public class MagazzinoEndpoints
{
    public static async Task<IResult> HandlePostIngredient(
        IIngredientsService ingredientsService,
        IngredientsJson body)
    {
        if (string.IsNullOrEmpty(body.IngredientId))
            body.IngredientId = Guid.NewGuid().ToString();

        await ingredientsService.AddIngredientAsync(body);

        return Results.Accepted();
    }

    public static async Task<IResult> HandlePutInventory(
        IIngredientsService ingredientsService,
        string ingredientId,
        IngredientsJson body)
    {
        await ingredientsService.AddIngredientAvailabilityAsync(body);

        return Results.NoContent();
    }
}