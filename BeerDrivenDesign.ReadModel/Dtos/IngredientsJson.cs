namespace BeerDrivenDesign.ReadModel.Dtos;

public class IngredientsJson
{
    public string IngredientId { get; set; } = string.Empty;
    public string IngredientName { get; set; } = string.Empty;
    public double Availability { get; set; } = 0;
}