using BeerDrivenDesign.Api.Transport.RabbitMq.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Commands;
using BeerDrivenDesign.Modules.Produzione.Domain.CommandHandlers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BeerDrivenDesign.Api.Transport.RabbitMq.Consumers.Commands;

public sealed class BrewBeerCommandConsumer : MassTransitCommandConsumer<BrewBeerCommand>
{
    protected override ICommandHandlerAsync<BrewBeerCommand> HandlerAsync { get; }

    public BrewBeerCommandConsumer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
    {
        var repository = serviceProvider.GetService<IRepository>();
        if (repository is null)
            throw new ArgumentNullException(nameof(repository));

        HandlerAsync = new BrewBeerCommandHandler(repository, loggerFactory);
    }
    
    public override async Task Consume(ConsumeContext<BrewBeerCommand> context)
    {
        await HandlerAsync.HandleAsync(context.Message);
    }
}