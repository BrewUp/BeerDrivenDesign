namespace BeerDrivenDesign.Api.Modules;

public sealed class CorsModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public CorsModule()
    {
        IsEnabled = true;
        Order = 0;
    }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
                corsBuilder.SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
        });

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}