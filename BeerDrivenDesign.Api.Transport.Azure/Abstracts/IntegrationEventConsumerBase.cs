using MassTransit;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Api.Transport.Azure.Abstracts
{
    public abstract class IntegrationEventConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : IntegrationEvent
    {
        protected abstract IEnumerable<IIntegrationEventHandlerAsync<TEvent>> HandlersAsync { get; }
        public abstract Task Consume(ConsumeContext<TEvent> context);
    }
}