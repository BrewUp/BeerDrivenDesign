using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
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