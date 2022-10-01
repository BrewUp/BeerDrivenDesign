namespace BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

public record PostProductionBeer(Guid BatchId, string BatchNumber, Guid BeerId, string BeerType, double Quantity, DateTime ProductionTime);