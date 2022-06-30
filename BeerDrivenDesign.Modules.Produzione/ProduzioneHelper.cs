using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Produzione;

public static class ProduzioneHelper
{
    public static IServiceCollection AddProduzione(this IServiceCollection services)
    {
        services.AddScoped<IProduzioneService, ProduzioneService>();

        return services;
    }
}