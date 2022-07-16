﻿using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BeerDrivenDesign.Modules.Produzione.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandWithExceptionTest : CommandSpecification<BottlingBeerCommand>
{
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly Quantity _initialQuantity = new(30);
    private readonly BottleHalfLitre _bottleHalfLitre = new(50);
    private readonly Quantity _residualQuantity = new(5);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerBrewedEvent(_beerId, _initialQuantity);
        yield return new BeerBottled(_beerId, _bottleHalfLitre, _residualQuantity);
    }

    protected override BottlingBeerCommand When()
    {
        return new BottlingBeerCommand(_beerId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeerCommand> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new ProductionExceptionHappened(_beerId, "Non hai abbastanza birra!!!!");
    }
}