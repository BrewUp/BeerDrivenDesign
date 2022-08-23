using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Tests.Entities;

public class CompleteBeerProductionTest : CommandSpecification<CompleteBeerProduction>
{
    private readonly BatchId _batchId = new(Guid.NewGuid());
    private readonly BatchNumber _batchNumber = new("2022-125");

    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerType _beerType = new("IPA");

    private readonly Quantity _quantity = new(200);
    private readonly ProductionStartTime _productionStartTime = new(DateTime.UtcNow);
    private readonly ProductionCompleteTime _productionCompleteTime = new(DateTime.UtcNow.AddDays(1));
    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionStarted(_beerId, _beerType, _batchId, _batchNumber, _quantity, _productionStartTime);
    }

    protected override CompleteBeerProduction When()
    {
        return new CompleteBeerProduction(_beerId, _batchNumber, _quantity, _productionCompleteTime);
    }

    protected override ICommandHandlerAsync<CompleteBeerProduction> OnHandler()
    {
        return new CompleteBeerProductionCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerProductionCompleted(_beerId, _batchNumber, _quantity, _productionCompleteTime);
    }
}