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
using BeerDrivenDesign.Modules.Produzione.Concretes;

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

        builder.Services.AddScoped<IBeerService, BeerService>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var repository = serviceProvider.GetService<IRepository>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        var beerService = serviceProvider.GetService<IBeerService>();

        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(repository, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(StartBeerProductionCommand)), loggerFactory),
            
            new BrewBeerCommandConsumer(repository, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BrewBeerCommand)), loggerFactory),
            new BeerBrewedConsumer(beerService, new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], nameof(BeerBrewedEvent), "beerdriven-subscription"),
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