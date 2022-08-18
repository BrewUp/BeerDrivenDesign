using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;
using BeerDrivenDesign.Modules.Produzione.EventsHandlers;
using BeerDrivenDesign.Modules.Produzione.Factories;
using BeerDrivenDesign.Modules.Produzione.Validators;
using BeerDrivenDesign.ReadModel;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.Events;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages;
using Muflone.Messages.Events;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Factories;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IBeerService, BeerService>();

        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<BrewBeerValidator>());

        services.AddScoped<IDomainEventHandlerAsync<BeerBrewedEvent>, BeerBrewedEventHandler>();

        services.AddScoped<IIntegrationEventHandlerAsync<ProductionStarted>, ProductionStartedEventHandler>();
        services.AddScoped<IMessageMapper<ProductionStarted>, ProductionStartedMapper>();

        services.AddSingleton<IAzureQueueReferenceFactory, AzureQueueReferenceFactory>();

        return services;
    }

    //TODO: Dove mettiamo questa parte di Infrastructure? Ora è in SharedModule
    public static IServiceCollection AddProductionInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}