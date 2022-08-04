using BeerDrivenDesign.Api.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;

namespace BeerDrivenDesign.Api.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, EventStoreSettings eventStoreSettings)
    {
        services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

        return services;
    }
}