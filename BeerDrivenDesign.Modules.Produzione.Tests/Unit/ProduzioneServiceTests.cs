using BeerDrivenDesign.Modules.Produzione.Concretes;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeerDrivenDesign.Modules.Produzione.Tests.Unit;

public class ProduzioneServiceTests
{
    private readonly ProduzioneService _service;

    public ProduzioneServiceTests()
    {
        _service = new ProduzioneService(Mock.Of<ILoggerFactory>());
    }

    [Fact]
    public void GetOrders_ThrowsException()
    {
        Assert.Throws<NotImplementedException>(() => _service.GetOrders());
    }
}