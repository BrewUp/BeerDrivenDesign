using MassTransit;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Api.Transport.RabbitMq.Abstracts
{
    public abstract class DomainEventConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : DomainEvent
    {
        protected abstract IEnumerable<IDomainEventHandlerAsync<TEvent>> HandlersAsync { get; }

        public abstract Task Consume(ConsumeContext<TEvent> context);
    }
}