using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeerDrivenDesign.Modules.Produzione.Tests.Unit;

public class ProduzioneServiceTests
{
    private readonly ProductionService _service;

    public ProduzioneServiceTests()
    {
        _service = new ProductionService(Mock.Of<ILoggerFactory>());
    }

    [Fact]
    public void GetOrders_ThrowsException()
    {
        Assert.Throws<NotImplementedException>(() => _service.Brew(new BrewBeer("1", 0)));
    }
}