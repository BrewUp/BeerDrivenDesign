using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.ReadModel.Models;

public class Recipes : ModelBase
{
    public string Description { get; private set; } = string.Empty;
    public IEnumerable<IngredientsJson> Ingredients { get; private set; } = Enumerable.Empty<IngredientsJson>();

    protected Recipes()
    {}

    public static Recipes CreateRecipes(string description, IEnumerable<IngredientsJson> ingredients) =>
        new(description, ingredients);

    private Recipes(string description, IEnumerable<IngredientsJson> ingredients)
    {
        Id = Guid.NewGuid().ToString();
        Description = description;
        Ingredients = ingredients;
    }

    public RecipesJson ToJson() => new()
    {
        Id = Id,
        Description = Description,
        Ingredients = Ingredients
    };
}