using BeerDrivenDesign.Modules.Produzione.Endpoints;
using BeerDrivenDesign.Modules.Produzione.Hubs;

namespace BeerDrivenDesign.Api.Modules;

public class SignalrModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("v1/signalr/", ProductionEndpoints.HandleGetSignalR)
            .WithTags("SignalR");

        endpoints.MapHub<ProductionHub>("/hubs/production");

        return endpoints;
    }
}