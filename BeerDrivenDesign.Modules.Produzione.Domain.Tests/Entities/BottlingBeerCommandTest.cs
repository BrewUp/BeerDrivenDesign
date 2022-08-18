using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandTest : CommandSpecification<BottlingBeerCommand>
{
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly Quantity _initialQuantity = new(30);
    private readonly BottleHalfLitre _bottleHalfLitre = new(50);
    private readonly Quantity _residualQuantity = new(5);
    private readonly Quantity _finalQuantity = new(-25);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerBrewedEvent(_beerId, _initialQuantity);
    }

    protected override BottlingBeerCommand When()
    {
        return new BottlingBeerCommand(_beerId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeerCommand> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerBottled(_beerId, _bottleHalfLitre, _residualQuantity);
    }
}