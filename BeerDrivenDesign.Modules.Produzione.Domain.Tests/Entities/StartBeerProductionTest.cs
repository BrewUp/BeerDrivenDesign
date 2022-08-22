using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class StartBeerProductionTest : CommandSpecification<StartBeerProduction>
{
    private readonly BatchId _batchId = new("2022-125");

    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerType _beerType = new("IPA");

    private readonly Quantity _quantity = new(200);
    private readonly ProductionStartTime _productionStartTime = new(DateTime.UtcNow);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield break;
    }

    protected override StartBeerProduction When()
    {
        return new StartBeerProduction(_beerId, _batchId, _beerType, _quantity, _productionStartTime);
    }

    protected override ICommandHandlerAsync<StartBeerProduction> OnHandler()
    {
        return new StartBeerProductionCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerProductionStarted(_beerId, _beerType, _batchId, _quantity, _productionStartTime);
    }
}