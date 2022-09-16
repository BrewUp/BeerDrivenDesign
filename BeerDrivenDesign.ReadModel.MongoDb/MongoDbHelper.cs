using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.ReadModel.Abstracts;
using BeerDrivenDesign.ReadModel.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BeerDrivenDesign.ReadModel.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbSettings mongoDbParameter)
    {
        services.AddSingleton<IMongoClient>(provider => new MongoClient(mongoDbParameter.ConnectionString));
        services.AddScoped(provider =>
            provider.GetService<IMongoClient>()
                ?.GetDatabase(mongoDbParameter.DatabaseName)
                .WithWriteConcern(WriteConcern.W1));

        services.AddScoped<IPersister, Persister>();

        return services;
    }
}