using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using BrewUp.Shared.Messages.Commands;
using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;

public class StartBeerProductionCommandHandler : CommandHandlerAsync<StartBeerProduction>
{
    public StartBeerProductionCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(StartBeerProduction command, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        /* Mi arriva il comando
         * Sono quindi alla porta di ingresso del domino
         * - devo validarlo (posso rifiutarlo)
         * - devo andare dall'aggregato Birra_i3d_Autunno e dirgli che è partita la produzione
         * - 
         */
        try
        {
            var beer = Beer.StartBeerProduction(new BeerId(command.AggregateId.Value), command.BeerType,
                command.BatchId, command.Quantity, command.ProductionStartTime);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(new BeerId(command.AggregateId.Value), ex);
        }
    }
}