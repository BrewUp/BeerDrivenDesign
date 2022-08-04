using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class CompleteBeerProductionTest : CommandSpecification<CompleteBeerProductionCommand>
{
    private readonly BatchId _batchId = new("2022-125");

    private readonly BeerId _beerId = new(Guid.NewGuid());

    private readonly Quantity _quantity = new(200);
    private readonly ProductionStartTime _productionStartTime = new(DateTime.UtcNow);
    private readonly ProductionCompleteTime _productionCompleteTime = new(DateTime.UtcNow.AddDays(1));
    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionStarted(_beerId, _batchId, _quantity, _productionStartTime);
    }

    protected override CompleteBeerProductionCommand When()
    {
        return new CompleteBeerProductionCommand(_batchId, _beerId, _quantity, _productionCompleteTime);
    }

    protected override ICommandHandlerAsync<CompleteBeerProductionCommand> OnHandler()
    {
        return new CompleteBeerProductionCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerProductionCompleted(_beerId, _batchId, _quantity, _productionCompleteTime);
    }
}