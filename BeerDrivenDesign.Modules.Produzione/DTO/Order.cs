namespace BeerDrivenDesign.Modules.Produzione.DTO;

public record Order
{
    public Order(int id)
    {
        Id = id;
    }
    
    public int Id { get; init; }
}