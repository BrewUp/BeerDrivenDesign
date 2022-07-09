namespace BeerDrivenDesign.ReadModel.Models;

public class Ingredients
{
    public double Water { get; private set; } = double.MinValue;
    public double Hop { get; private set; } = double.MinValue;
    public double Yeast { get; private set; } = double.MinValue;
    public double BarleyMalt { get; private set; } = double.MinValue;
}