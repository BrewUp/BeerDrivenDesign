using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.ReadModel.Models;

public class IngredientsInventories : ModelBase
{
    public double Availability { get; private set; } = 0;

    protected IngredientsInventories()
    {}


    public static IngredientsInventories CreateInventories(string ingredientId, double availability) =>
        new(ingredientId, availability);

    private IngredientsInventories(string ingredientId, double availability)
    {
        Id = ingredientId;
        Availability = availability;
    }
}