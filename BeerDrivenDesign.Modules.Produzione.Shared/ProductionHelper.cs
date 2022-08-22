using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Modules.Produzione.Shared.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeerDrivenDesign.Modules.Produzione.Shared;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<BrewBeerValidator>());

        //services.AddSingleton<IAzureQueueReferenceFactory, AzureQueueReferenceFactory>();

        return services;
    }
}