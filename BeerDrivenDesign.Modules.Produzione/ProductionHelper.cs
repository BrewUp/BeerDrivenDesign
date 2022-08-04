using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.EventsHandlers;
using BeerDrivenDesign.Modules.Produzione.Validators;
using BeerDrivenDesign.ReadModel;
using BrewUp.Shared.Messages.Events;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services, string servicebusConnectionString)
    {
        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<BrewBeerValidator>());

        services.AddScoped<IIntegrationEventHandlerAsync<ProductionStarted>, ProductionStartedEventHandler>();
        services.AddScoped<IMessageMapper<ProductionStarted>, ProductionStartedMapper>();

        return services;
    }
}