using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.ReadModel.Models;

public class Ingredients : ModelBase
{
    public string Name { get; private set; } = string.Empty;    

    protected Ingredients()
    {}

    public static Ingredients CreateIngredients(string ingredientId, string name) => new(ingredientId, name);

    private Ingredients(string ingredientId, string ingredientName)
    {
        Id = ingredientId;
        Name = ingredientName;
    }
}