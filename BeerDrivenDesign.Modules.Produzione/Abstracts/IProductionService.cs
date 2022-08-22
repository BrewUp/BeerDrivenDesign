using BeerDrivenDesign.Modules.Produzione.Shared.DTO;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionService
{
    Task Brew(BrewBeer command);
}