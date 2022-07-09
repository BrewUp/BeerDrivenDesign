using BeerDrivenDesign.Api.Shared.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BeerDrivenDesign.Api.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddSharedService(this IServiceCollection services)
    {
        services.AddScoped<IServiceBus, InProcessServiceBus>();

        return services;
    }
}