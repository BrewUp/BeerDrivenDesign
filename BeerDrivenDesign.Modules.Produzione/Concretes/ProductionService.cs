using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using BeerDrivenDesign.Modules.Produzione.DTO;
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

    public async Task Brew(BrewBeer body)
    {
        var command = new BrewBeerCommand(
            new BeerId(body.BeerId),
            new Quantity(body.Quantity),
            new BeerType(body.BeerType),
            new HopQuantity(body.HopQuantity)
        );

        await _serviceBus.SendAsync(command);
    }
}