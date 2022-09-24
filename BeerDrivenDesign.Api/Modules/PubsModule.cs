using BeerDrivenDesign.Modules.Pubs;
using BeerDrivenDesign.Modules.Pubs.Endpoints;

namespace BeerDrivenDesign.Api.Modules;

public class PubsModule : IModule
{
    private const string BaseEndpointUrl = "v1/pubs";
    private const string BaseTags = "Pubs";

    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddPubs();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{BaseEndpointUrl}/beers", PubsEndpoints.HandleGetBeers)
            .WithTags(BaseTags);

        return endpoints;
    }
}