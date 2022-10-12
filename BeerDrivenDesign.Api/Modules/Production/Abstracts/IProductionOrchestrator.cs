using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Production.Abstracts;

public interface IProductionOrchestrator
{
    Task StartProductionAsync(PostProductionBeer postBrewBeer);
    Task CompleteProductionAsync(PostProductionBeer postBrewBeer);
}