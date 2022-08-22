namespace BeerDrivenDesign.Modules.Produzione.Shared.DTO;

public record BrewBeer(Guid BeerId, string BeerType, double Quantity, int HopQuantity);