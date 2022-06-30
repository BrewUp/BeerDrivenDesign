namespace BeerDrivenDesign.Api.Modules;

public class CommercialeModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        throw new NotImplementedException();
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        throw new NotImplementedException();
    }
}