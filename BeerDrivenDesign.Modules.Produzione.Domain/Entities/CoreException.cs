using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.Domain.Entities;

public class CoreException : AggregateRoot
{
    private string _message = string.Empty;

    protected CoreException()
    {
    }

    internal static CoreException CreateAggregateException(BatchId aggregateId, Exception ex)
    {
        return new CoreException(aggregateId, ex);
    }

    private CoreException(BatchId aggregateId, Exception ex)
    {
        RaiseEvent(new ProductionExceptionHappened(aggregateId,
            $"StackTrace: {ex.StackTrace} - Source: {ex.Source} - Message: {ex.Message}"));
    }

    private void Apply(ProductionExceptionHappened @event)
    {
        Id = @event.AggregateId;

        _message = @event.Message;
    }
}