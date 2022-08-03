using BeerDrivenDesign.Api.Shared;
using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Api.Transport.Azure;
using BeerDrivenDesign.Api.Transport.Azure.Settings;
using BeerDrivenDesign.Api.Transport.RabbitMq;
using BeerDrivenDesign.Api.Transport.RabbitMq.Settings;
using BeerDrivenDesign.ReadModel.MongoDb;
using Serilog;

namespace BeerDrivenDesign.Api.Modules;

public class SharedModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 99;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs\\BeerDriven.log")
            .CreateLogger();

        if (builder.Configuration["BrewUp:BrokerOptions:Type"].Equals("Azure"))
        {
            var serviceBusOptions = new ServiceBusOptions();
            builder.Configuration.GetSection("BrewUp:ServiceBusSettings").Bind(serviceBusOptions);
            builder.Services.AddAzureTransport(serviceBusOptions);
        }

        if (builder.Configuration["BrewUp:BrokerOptions:Type"].Equals("RMQ"))
        {
            var rmqSettings = new RabbitMqSettings();
            builder.Configuration.GetSection("BrewUp:RabbitMQSettings").Bind(rmqSettings);
            builder.Services.AddRmqTransport(rmqSettings);
        }

        builder.Services.AddSharedService(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());
        builder.Services.AddMongoDb(builder.Configuration["BrewUp:MongoDbSettings:ConnectionString"]);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}