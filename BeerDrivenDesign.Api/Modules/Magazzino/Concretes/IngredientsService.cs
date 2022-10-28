using BeerDrivenDesign.Api.Modules.Magazzino.Abstracts;
using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;
using BeerDrivenDesign.ReadModel.Models;

namespace BeerDrivenDesign.Api.Modules.Magazzino.Concretes;

public sealed class IngredientsService : StoreBaseService, IIngredientsService
{
    public IngredientsService(IPersister persister,
        ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }

    public async Task AddIngredientAsync(IngredientsJson ingredientToAdd)
    {
        try
        {
            var ingredient = Ingredients.CreateIngredients(ingredientToAdd.IngredientId, ingredientToAdd.IngredientName);
            await Persister.InsertAsync(ingredient);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task AddIngredientAvailabilityAsync(IngredientsJson availability)
    {
        try
        {
            var inventory =
                IngredientsInventories.CreateInventories(availability.IngredientId, availability.Availability);
            await Persister.InsertAsync(inventory);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IngredientsJson> GetIngredientAvailabilityAsync(string ingredientId)
    {
        try
        {
            var ingredient = await Persister.GetByIdAsync<Ingredients>(ingredientId);
            var inventory = await Persister.GetByIdAsync<IngredientsInventories>(ingredientId);

            return new IngredientsJson
            {
                IngredientId = ingredientId,
                IngredientName = ingredient.Name,
                Availability = inventory.Availability
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}