using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Magazzino.Abstracts;

public interface IIngredientsService
{
    Task AddIngredientAsync(IngredientsJson ingredientToAdd);
    Task AddIngredientAvailabilityAsync(IngredientsJson availability);

    Task<IngredientsJson> GetIngredientAvailabilityAsync(string ingredientId);
}