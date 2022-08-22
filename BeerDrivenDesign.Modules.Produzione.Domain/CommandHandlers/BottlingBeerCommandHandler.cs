using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;

public sealed class BottlingBeerCommandHandler : CommandHandlerAsync<BottlingBeer>
{
    public BottlingBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(BottlingBeer command, CancellationToken cancellationToken = new())
    {
        try
        {
            var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
            beer.BottlingBeer(command.BeerId, command.BottleHalfLitre);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BeerId, ex);
        }
    }
}