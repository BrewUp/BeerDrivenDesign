using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Modules.Produzione;
using BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;
using BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;
using BeerDrivenDesign.ReadModel.MongoDb;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.Events;
using Muflone.Factories;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using Muflone.Transport.Azure;
using Muflone.Persistence;

namespace BeerDrivenDesign.Api.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 98;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProductionModule();

        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var repository = serviceProvider.GetService<IRepository>();

        var clientId = builder.Configuration["BrewUp:ClientId"];
        var serviceBusConnectionString = builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"];
        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(repository!, new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(StartBeerProduction), clientId), loggerFactory!),
            new BeerProductionStartedConsumer(domainEventHandlerFactoryAsync!, new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(BeerProductionStarted), clientId), loggerFactory!),

            new CompleteBeerProductionConsumer(repository!, new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(CompleteBeerProduction), clientId), loggerFactory!),
            new BeerProductionCompletedConsumer(domainEventHandlerFactoryAsync!, new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(BeerProductionCompleted), clientId), loggerFactory!)
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