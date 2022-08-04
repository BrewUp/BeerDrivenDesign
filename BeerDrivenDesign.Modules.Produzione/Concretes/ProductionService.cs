using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.DTO;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    private readonly IServiceBus _serviceBus;
    private readonly IEventBus _eventBus;

    public ProductionService(ILoggerFactory loggerFactory, IServiceBus serviceBus, IEventBus eventBus) : base(loggerFactory)
    {
        _serviceBus = serviceBus;
        _eventBus = eventBus;
    }

    public async Task Brew(BrewBeer body)
    {
        var command = new BrewBeerCommand(
            body.BeerId,
            new Quantity(body.Quantity),
            new BeerType(body.BeerType),
            new HopQuantity(body.HopQuantity)
        );

        await _serviceBus.SendAsync(command);

        //var productionStarted = new ProductionStarted(new BeerId(Guid.NewGuid()),
        //    new ProductionStartTime(DateTime.UtcNow), new Quantity(100), new BatchId("123"));

        //await _eventBus.PublishAsync(productionStarted);
    }
}