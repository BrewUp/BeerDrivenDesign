using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone.Transport.Azure.Extensions;

namespace BeerDrivenDesign.Modules.Produzione.Tests.Unit;

public class ProductionServiceTests
{
    [Fact]
    public void Can_Retrive_EventName()
    {
        var eventName = nameof(ProductionStarted).ToLower();

        Assert.Equal("productionstarted", eventName);
    }

    [Fact]
    public void Can_Serialize_And_Deserialize_A_Command()
    {
        var messageSerilizer = new MessageSerializer();

        var command = new StartBeerProduction(
            new BeerId(Guid.NewGuid()),
            new BatchId(Guid.NewGuid()),
            new BatchNumber("1234"),
            new BeerType("IPA"),
            new Quantity(100),
            new ProductionStartTime(DateTime.UtcNow)
        );

        var commandSerialized = messageSerilizer.Serialize(command);
        var commandDeserialized = messageSerilizer.Deserialize<StartBeerProduction>(commandSerialized);

        Assert.Equal(command.BatchId, commandDeserialized.BatchId);
    }
}