using BeerDrivenDesign.Modules.Pubs.Abstracts;
using BeerDrivenDesign.Modules.Pubs.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Pubs;

public static class PubsHelper
{
    public static IServiceCollection AddPubs(this IServiceCollection services)
    {
        services.AddScoped<IPubsService, PubsService>();

        return services;
    }
}