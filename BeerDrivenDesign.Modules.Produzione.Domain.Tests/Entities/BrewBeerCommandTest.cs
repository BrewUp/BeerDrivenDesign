using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BrewBeerCommandTest : CommandSpecification<BrewBeerCommand>
{
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly Quantity _quantity = new(1);
    private readonly BeerType _beerType = new("Pilsner");
    private readonly HopQuantity _hopQuantity = new(1);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield break;
    }

    protected override BrewBeerCommand When()
    {
        return new BrewBeerCommand(_beerId.Value, _quantity, _beerType, _hopQuantity);
    }

    protected override ICommandHandlerAsync<BrewBeerCommand> OnHandler()
    {
        return new BrewBeerCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerBrewedEvent(_beerId, _quantity);
    }
}