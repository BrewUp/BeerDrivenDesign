using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BeerDrivenDesign.Modules.Produzione.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    private readonly IServiceBus _serviceBus;

    public ProductionService(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
    {
        _serviceBus = serviceBus;
    }

    public async Task Brew(PostBrewBeer postBrewBeer)
    {
        var command = new StartBeerProduction(
            new BeerId(postBrewBeer.BeerId),
            new BatchId(postBrewBeer.BatchId),
            new BeerType(postBrewBeer.BeerType),
            new Quantity(postBrewBeer.Quantity),
            new ProductionStartTime(DateTime.UtcNow)
        );

        await _serviceBus.SendAsync(command);
    }
}