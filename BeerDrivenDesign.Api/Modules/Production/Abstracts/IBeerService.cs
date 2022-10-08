using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}