using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;
using BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;
using BeerDrivenDesign.ReadModel.MongoDb;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using Muflone.Transport.Azure;
using Serilog;
using Muflone.Factories;
using BeerDrivenDesign.Modules.Produzione.Factories;

namespace BeerDrivenDesign.Api.Modules;

public class SharedModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 98;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs\\BeerDriven.log")
            .CreateLogger();

        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddMongoDb(mongoDbSettings);

        builder.Services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        builder.Services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var commandHandlerFactoryAsync = serviceProvider.GetService<ICommandHandlerFactoryAsync>();

        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(commandHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(StartBeerProductionCommand)), loggerFactory),
            
            new BrewBeerCommandConsumer(commandHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BrewBeerCommand)), loggerFactory),
            new BeerBrewedConsumer(domainEventHandlerFactoryAsync!, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BeerBrewedEvent), "beerdriven-subscription"),
                loggerFactory)
        };
        builder.Services.AddMufloneTransportAzure(new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], ""),
            consumers);

        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());
        builder.Services.AddEventstoreMongoDb(builder.Configuration["BrewUp:MongoDbSettings:ConnectionString"]);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}