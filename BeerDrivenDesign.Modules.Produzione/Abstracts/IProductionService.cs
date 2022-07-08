using BeerDrivenDesign.Modules.Produzione.DTO;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProductionService
{
    void Brew(BrewBeer command);
}