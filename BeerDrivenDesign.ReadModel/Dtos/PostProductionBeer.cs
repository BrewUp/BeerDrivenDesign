namespace BeerDrivenDesign.ReadModel.Dtos;

public record PostProductionBeer(Guid BeerId, string BeerType, string BatchNumber, double Quantity, DateTime ProductionTime);