using BeerDrivenDesign.Modules.Produzione;
using BeerDrivenDesign.Modules.Produzione.Domain;
using BeerDrivenDesign.Modules.Produzione.Endpoints;

namespace BeerDrivenDesign.Api.Modules;

public class ProductionModule : IModule
{
    private const string BaseEndpointUrl = "/production";

    public bool IsEnabled => true;
    public int Order => 99;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProduction();
        builder.Services.AddProductionDomain(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"]);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{BaseEndpointUrl}/beer/brew", ProductionEndpoints.HandleBrewBeer);

        return endpoints;
    }
}