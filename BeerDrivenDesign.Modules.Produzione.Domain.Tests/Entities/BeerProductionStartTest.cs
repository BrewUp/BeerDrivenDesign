using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class BeerProductionStartTest : CommandSpecification<StartBeerProductionCommand>
{
    private readonly BatchId _batchId = new ("2022-125");

    private readonly BeerId _beerId = new(Guid.NewGuid());

    private readonly Quantity _quantity = new(200);

    protected override IEnumerable<DomainEvent> Given()
    {
        /* Da Produzione arriva un evento di integrazione con 
            - Lotto
            - Tipo birra
            - Quantità
         */
        yield break;
    }

    protected override StartBeerProductionCommand When()
    {
        return new StartBeerProductionCommand(_batchId, _beerId, _quantity);
    }

    protected override ICommandHandlerAsync<StartBeerProductionCommand> OnHandler()
    {
        return new StartBeerProductionCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerProductionStarted(_beerId, _batchId, _quantity);
    }
}