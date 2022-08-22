namespace BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

public record PostBrewBeer(Guid BeerId, string BeerType, string BatchId, double Quantity, DateTime ProductionTime);