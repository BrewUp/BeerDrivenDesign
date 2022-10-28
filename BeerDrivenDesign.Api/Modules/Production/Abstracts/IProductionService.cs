using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Production.Abstracts;

public interface IProductionService
{
    Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync();
    Task CreateRecipesAsync(RecipesJson recipeToCreate);

    Task<RecipesJson> GetRecipeAsync(string beerId);
}