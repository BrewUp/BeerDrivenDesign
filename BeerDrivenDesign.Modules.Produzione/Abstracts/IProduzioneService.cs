using BeerDrivenDesign.Modules.Produzione.DTO;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public interface IProduzioneService
{
    IEnumerable<Order> GetOrders();
}