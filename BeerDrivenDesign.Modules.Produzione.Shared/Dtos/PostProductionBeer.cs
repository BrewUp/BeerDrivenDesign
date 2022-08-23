namespace BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

public record PostProductionBeer(Guid BeerId, string BeerType, string BatchNumber, double Quantity, DateTime ProductionTime);