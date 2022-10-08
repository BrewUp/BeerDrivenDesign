using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionOrchestrator
{
    Task StartProductionAsync(PostProductionBeer postBrewBeer);
    Task CompleteProductionAsync(PostProductionBeer postBrewBeer);
}