using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.DTO;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProductionService : ProduzioneBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public void Brew(BrewBeer command)
    {
        
        throw new NotImplementedException();
    }
}