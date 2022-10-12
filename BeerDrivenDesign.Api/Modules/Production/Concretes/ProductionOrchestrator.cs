using BeerDrivenDesign.Api.Modules.Production.Abstracts;
using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Production.Concretes;

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