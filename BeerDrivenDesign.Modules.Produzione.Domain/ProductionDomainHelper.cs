using BeerDrivenDesign.Modules.Produzione.Domain.Consumers.Commands;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Modules.Produzione.Domain;

public static class ProductionDomainHelper
{
    public static IServiceCollection AddProductionDomain(this IServiceCollection services,
        string azureServiceBusConnectionString)
    {


        return services;
    }
}