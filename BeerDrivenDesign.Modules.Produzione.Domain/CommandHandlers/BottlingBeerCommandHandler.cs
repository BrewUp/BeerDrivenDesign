using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;

public sealed class BottlingBeerCommandHandler : CommandHandlerAsync<BottlingBeerCommand>
{
    public BottlingBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(BottlingBeerCommand command, CancellationToken cancellationToken = new())
    {
        try
        {
            var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
            beer.BottlingBeer(command.BeerId, command.BottleHalfLitre);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}