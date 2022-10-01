using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandTest : CommandSpecification<BottlingBeer>
{
    private readonly BatchId _batchId = new(Guid.NewGuid());
    private readonly BatchNumber _batchNumber = new("1234");

    private readonly BeerId _beerId = new(Guid.NewGuid());

    private readonly BottleHalfLitre _bottleHalfLitre = new(50);
    private readonly Quantity _residualQuantity = new(5);
    private readonly Quantity _finalQuantity = new(30);
    private readonly BeerLabel _beerLabel = new("Label");

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionCompleted(_batchId, _batchNumber, _beerId, _finalQuantity,
            new ProductionCompleteTime(DateTime.UtcNow));
    }

    protected override BottlingBeer When()
    {
        return new BottlingBeer(_batchId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeer> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerBottledV2(_batchId, _bottleHalfLitre, _residualQuantity, _beerLabel);
    }
}