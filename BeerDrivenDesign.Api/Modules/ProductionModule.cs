using BeerDrivenDesign.Modules.Produzione.Endpoints;
using BeerDrivenDesign.Modules.Produzione.Shared;

namespace BeerDrivenDesign.Api.Modules;

public class ProductionModule : IModule
{
    private const string BaseEndpointUrl = "v1/production";

    public bool IsEnabled => true;
    public int Order => 99;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProduction();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{BaseEndpointUrl}/beers/brew", ProductionEndpoints.HandleStartProduction);
        endpoints.MapPut("v1/production/beers/brew/{productionNumber}", ProductionEndpoints.HandleCompleteProduction);

        endpoints.MapGet($"{BaseEndpointUrl}/beers", ProductionEndpoints.HandleGetBeers);
        endpoints.MapGet($"{BaseEndpointUrl}", ProductionEndpoints.HandleGetProductionOrders);

        return endpoints;
    }
}