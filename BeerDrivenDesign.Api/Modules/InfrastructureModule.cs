﻿using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;
using BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;
using BeerDrivenDesign.ReadModel.MongoDb;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.Events;
using Muflone.Factories;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using Muflone.Transport.Azure;
using BeerDrivenDesign.Modules.Produzione.Factories;
using BeerDrivenDesign.Modules.Produzione.EventsHandlers;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BeerDrivenDesign.Api.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 98;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductionService, ProductionService>();
        builder.Services.AddScoped<IBeerService, BeerService>();

        builder.Services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        builder.Services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        builder.Services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionStarted>, BeerProductionStartedEventHandler>();

        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var repository = serviceProvider.GetService<IRepository>();

        var clientId = builder.Configuration["BrewUp:ClientId"];
        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(repository!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(StartBeerProduction), clientId), loggerFactory),
            new BeerProductionStartedConsumer(domainEventHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BeerProductionStarted), clientId), loggerFactory)
        };
        builder.Services.AddMufloneTransportAzure(
            new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], "",
                clientId), consumers);
        
        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddEventstoreMongoDb(mongoDbSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}