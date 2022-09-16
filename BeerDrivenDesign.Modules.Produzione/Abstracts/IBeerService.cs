using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}