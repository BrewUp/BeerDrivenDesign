using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Modules.Produzione.Consumers.DomainEvents;
using BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;
using BeerDrivenDesign.ReadModel.MongoDb;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.Events;
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

        //TODO: Spostare in apposito Helper
        builder.Services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        builder.Services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var commandHandlerFactoryAsync = serviceProvider.GetService<ICommandHandlerFactoryAsync>();

        var clientId = "BeerDrivenDesign";
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
        builder.Services.AddEventstoreMongoDb(builder.Configuration["BrewUp:MongoDbSettings:ConnectionString"]);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}