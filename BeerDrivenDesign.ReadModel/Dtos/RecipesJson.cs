namespace BeerDrivenDesign.ReadModel.Dtos;

public class RecipesJson
{
    public string Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public IEnumerable<IngredientsJson> Ingredients { get; set; } = Enumerable.Empty<IngredientsJson>();
}