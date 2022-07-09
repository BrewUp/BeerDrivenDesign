using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BeerDrivenDesign.Modules.Produzione.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

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
        return new BrewBeerCommand(_beerId, _quantity, _beerType, _hopQuantity);
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