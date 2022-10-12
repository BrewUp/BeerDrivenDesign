using BeerDrivenDesign.ReadModel.Dtos;

namespace BeerDrivenDesign.Api.Modules.Production.Abstracts;

public interface IBeerService
{
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}