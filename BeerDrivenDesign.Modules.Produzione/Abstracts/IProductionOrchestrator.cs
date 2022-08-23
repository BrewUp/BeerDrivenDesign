using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionOrchestrator
{
    Task StartProductionAsync(PostProductionBeer postBrewBeer);
    Task CompleteProductionAsync(PostProductionBeer postBrewBeer);
}