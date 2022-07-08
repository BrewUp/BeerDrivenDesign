using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.CustomTypes;
using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;

public class BrewBeerCommandHandler : CommandHandlerAsync<BrewBeerCommand>
{
    public BrewBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository,
        loggerFactory)
    {
    }

    public override async Task HandleAsync(BrewBeerCommand command,
        CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();

        var beer = Beer.CreateBeer(new BeerId(command.AggregateId.Value), command.Quantity);

        await Repository.SaveAsync(beer, Guid.NewGuid());
    }
}