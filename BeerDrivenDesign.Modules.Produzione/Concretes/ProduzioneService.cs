using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.DTO;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProduzioneService : ProduzioneBaseService, IProduzioneService
{
    public ProduzioneService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public IEnumerable<Order> GetOrders()
    {
        throw new NotImplementedException();
    }
}