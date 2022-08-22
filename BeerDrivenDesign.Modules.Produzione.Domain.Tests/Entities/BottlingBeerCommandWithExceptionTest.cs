using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandWithExceptionTest : CommandSpecification<BottlingBeer>
{
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BatchId _batchId = new("1234");
    private readonly Quantity _initialQuantity = new(30);
    private readonly BottleHalfLitre _bottleHalfLitre = new(50);
    private readonly Quantity _residualQuantity = new(5);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionCompleted(_beerId, _batchId, _initialQuantity,
            new ProductionCompleteTime(DateTime.UtcNow));
        yield return new BeerBottled(_beerId, _bottleHalfLitre, _residualQuantity);
    }

    protected override BottlingBeer When()
    {
        return new BottlingBeer(_beerId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeer> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new ProductionExceptionHappened(_beerId, "Non hai abbastanza birra!!!!");
    }
}