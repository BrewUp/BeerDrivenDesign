using BeerDrivenDesign.ReadModel.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Eventstore.Persistence;

namespace BeerDrivenDesign.ReadModel.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IEventStorePositionRepository>(x =>
            new EventStorePositionRepository(x.GetService<ILogger<EventStorePositionRepository>>(), connectionString));
        return services;
    }
}