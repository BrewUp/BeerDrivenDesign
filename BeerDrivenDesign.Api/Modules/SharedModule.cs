using Serilog;

namespace BeerDrivenDesign.Api.Modules;

public class SharedModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 97;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs\\BeerDriven.log")
            .CreateLogger();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}