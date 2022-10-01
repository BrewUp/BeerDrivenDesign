using BeerDrivenDesign.Modules.Produzione.Domain.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Domain.Entities;
using BrewUp.Shared.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;

public class CompleteBeerProductionCommandHandler : CommandHandlerAsync<CompleteBeerProduction>
{
    public CompleteBeerProductionCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(CompleteBeerProduction command, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var order = await Repository.GetByIdAsync<Order>(command.AggregateId.Value);
            order.CompleteBeerProduction(command.Quantity, command.ProductionCompleteTime);

            await Repository.SaveAsync(order, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BatchId, ex);
        }
    }
}