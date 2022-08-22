using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionService
{
    Task Brew(PostBrewBeer postBrewBeer);
}