using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.EventsHandlers;
using BeerDrivenDesign.Modules.Produzione.Factories;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using BrewUp.Shared.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Modules.Produzione;

public static class ProductionHelper
{
    public static IServiceCollection AddProductionModule(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrchestrator, ProductionOrchestrator>();

        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IBeerService, BeerService>();

        services.AddSingleton<ProductionHub>();

        #region DomainEventHandler
        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionStarted>, BeerProductionStartedEventHandler>();
        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionStarted>, BeerProductionStartedForProductionOrderEventHandler>();

        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionCompleted>, BeerProductionCompletedEventHandler>();
        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionCompleted>, BeerProductionCompletedForProductionOrderEventHandler>();
        #endregion

        return services;
    }
}