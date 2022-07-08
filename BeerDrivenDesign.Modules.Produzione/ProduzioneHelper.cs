using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Validators;
using BeerDrivenDesign.ReadModel;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Produzione;

public static class ProduzioneHelper
{
    public static IServiceCollection AddProduzione(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<BrewBeerValidator>());

        return services;
    }
}