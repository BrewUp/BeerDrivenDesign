using BrewUp.Shared.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Tests.Unit;

public class ProductionServiceTests
{
    [Fact]
    public void Can_Retrive_EventName()
    {
        var eventName = nameof(ProductionStarted).ToLower();

        Assert.Equal("productionstarted", eventName);
    }
}