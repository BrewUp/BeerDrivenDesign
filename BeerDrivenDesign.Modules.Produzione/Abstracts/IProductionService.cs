using BeerDrivenDesign.Modules.Produzione.DTO;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionService
{
    Task Brew(BrewBeer command);
}