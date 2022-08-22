using BeerDrivenDesign.Api.Shared.Configuration;
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
using Muflone.Messages;
using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using Muflone.Messages.Commands;

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

        builder.Services.AddScoped<IDomainEventHandlerAsync<BeerBrewedEvent>, BeerBrewedEventHandler>();
        builder.Services.AddScoped<IIntegrationEventHandlerAsync<ProductionStarted>, ProductionStartedEventHandler>();
        builder.Services.AddScoped<IMessageMapper<ProductionStarted>, ProductionStartedMapper>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var commandHandlerFactoryAsync = serviceProvider.GetService<ICommandHandlerFactoryAsync>();

        var clientId = builder.Configuration["BrewUp:ClientId"];
        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(commandHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(StartBeerProductionCommand), clientId), loggerFactory),

            new BrewBeerCommandConsumer(commandHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BrewBeerCommand), clientId), loggerFactory),
            new BeerBrewedConsumer(domainEventHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BeerBrewedEvent), clientId), loggerFactory)
        };
        builder.Services.AddMufloneTransportAzure(
            new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], "",
                clientId), consumers);

        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());
        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddEventstoreMongoDb(mongoDbSettings);

        builder.Services.AddScoped<ICommandHandlerAsync<BrewBeerCommand>, BrewBeerCommandHandler>();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}