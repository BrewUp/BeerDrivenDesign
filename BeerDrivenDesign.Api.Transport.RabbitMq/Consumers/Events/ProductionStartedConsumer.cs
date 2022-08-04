using BeerDrivenDesign.Api.Transport.RabbitMq.Abstracts;
using BrewUp.Shared.Messages.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

namespace BeerDrivenDesign.Api.Transport.RabbitMq.Consumers.Events;

public sealed class ProductionStartedConsumer : IntegrationEventConsumerBase<ProductionStarted>
{
    protected override IEnumerable<IIntegrationEventHandlerAsync<ProductionStarted>> HandlersAsync { get; }

    public ProductionStartedConsumer(IServiceProvider serviceProvider)
    {
        HandlersAsync = serviceProvider.GetServices<IIntegrationEventHandlerAsync<ProductionStarted>>();
    }

    public override async Task Consume(ConsumeContext<ProductionStarted> context)
    {
        foreach (var handler in HandlersAsync)
        {
            await handler.HandleAsync(context.Message);
        }
    }
}