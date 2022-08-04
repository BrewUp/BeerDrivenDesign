using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Modules.Produzione.Domain;

public static class ProductionDomainHelper
{
    public static IServiceCollection AddProductionDomain(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandlerAsync<BrewBeerCommand>, BrewBeerCommandHandler>();
        services.AddScoped<ICommandHandlerAsync<BottlingBeerCommand>, BottlingBeerCommandHandler>();

        return services;
    }
}