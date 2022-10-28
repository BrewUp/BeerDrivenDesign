using BeerDrivenDesign.Api.Modules.Magazzino.Abstracts;
using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Production.Concretes;

public sealed class ProductionOrchestrator : IProductionOrchestrator
{
    private readonly IProductionService _productionService;
    private readonly IIngredientsService _ingredientsService;

    public ProductionOrchestrator(IProductionService productionService, IIngredientsService ingredientsService)
    {
        _productionService = productionService;
        _ingredientsService = ingredientsService;
    }

    public async Task StartProductionAsync(PostProductionBeer postBrewBeer)
    {
        // Check ingredients availability
        var recipe = await _productionService.GetRecipeAsync(postBrewBeer.BeerId.ToString());
        if (string.IsNullOrEmpty(recipe.Description))
            throw new Exception($"No Recipe was found for Beer {postBrewBeer.BeerType}");

        foreach (var ingredient in recipe.Ingredients)
        {
            var availability = await _ingredientsService.GetIngredientAvailabilityAsync(ingredient.IngredientId);
            if (availability.Availability <= 0)
                throw new Exception($"No Availability for Ingredient {ingredient.IngredientName}");

            var productionAvailability = postBrewBeer.Quantity * ingredient.Availability;
            if (availability.Availability < productionAvailability)
                throw new Exception($"No Availability for Ingredient {ingredient.IngredientName}");
        }

        // Create Order
    }

    public Task CompleteProductionAsync(PostProductionBeer postBrewBeer)
    {
        return Task.CompletedTask;
    }
}