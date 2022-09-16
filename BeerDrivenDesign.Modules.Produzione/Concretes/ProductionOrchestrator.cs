using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProductionOrchestrator : IProductionOrchestrator
{
    public ProductionOrchestrator()
    {
    }

    public Task StartProductionAsync(PostProductionBeer postBrewBeer)
    {
        return Task.CompletedTask;
    }

    public Task CompleteProductionAsync(PostProductionBeer postBrewBeer)
    {
        return Task.CompletedTask;
    }
}