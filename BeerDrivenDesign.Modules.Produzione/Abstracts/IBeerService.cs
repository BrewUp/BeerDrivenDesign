using BeerDrivenDesign.Modules.Produzione.Shared.DTO;
using BeerDrivenDesign.ReadModel.Models;
using BrewUp.Shared.Messages.CustomTypes;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task CreateBeerAsync(BeerId beerId, Quantity quantity, string beerType, Ingredients ingredients);
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}