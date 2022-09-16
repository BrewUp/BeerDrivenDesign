using Microsoft.OpenApi.Models;

namespace BeerDrivenDesign.Api.Modules;

public class SwaggerModule
{
    public static IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
        {
            Description = "BrewUp API",
            Title = "BrewUp Api",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "BrewUp.Api"
            }
        }));

        return builder.Services;
    }
}