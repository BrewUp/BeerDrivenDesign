using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;
using BeerDrivenDesign.ReadModel.Models;

namespace BeerDrivenDesign.Api.Modules.Production.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory, IPersister persister)
        : base(persister, loggerFactory)
    {
    }
    public async Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync()
    {
        try
        {
            var productionOrders = await Persister.FindAsync<ProductionOrder>();
            var ordersArray = productionOrders as ProductionOrder[] ??
                              productionOrders.OrderByDescending(p => p.Id).ToArray();

            return ordersArray.Any()
                ? ordersArray.Select(p => p.ToJson())
                : Enumerable.Empty<ProductionOrderJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task CreateRecipesAsync(RecipesJson recipeToCreate)
    {
        try
        {
            var canCreateRecipe = true;
            // Check ingredients
            foreach (var ingredient in recipeToCreate.Ingredients)
            {
                var chkIngredient = await Persister.GetByIdAsync<Ingredients>(ingredient.IngredientId);
                if (!string.IsNullOrEmpty(chkIngredient.Name))
                    continue;

                canCreateRecipe = false;
                break;
            }

            if (!canCreateRecipe)
                throw new Exception("Please Check your Ingredients!");
            var recipe = Recipes.CreateRecipes(recipeToCreate.Description, recipeToCreate.Ingredients);
            await Persister.InsertAsync(recipe);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<RecipesJson> GetRecipeAsync(string beerId)
    {
        try
        {
            var recipe = await Persister.GetByIdAsync<Recipes>(beerId);

            return recipe.ToJson();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}