using BeerDrivenDesign.Api.Transport.RabbitMq.Settings;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Muflone;
using Muflone.Messages;
using Muflone.Messages.Commands;

namespace BeerDrivenDesign.Api.Transport.RabbitMq;

public sealed class ServiceBus : IServiceBus, IEventBus, IHostedService
{
    private readonly IBusControl _busControl;
    private readonly ILogger<ServiceBus> _logger;

    private readonly string _queueName = string.Empty;

    public ServiceBus(IBusControl busControl,
        ILogger<ServiceBus> logger,
        IOptions<RabbitMqSettings> options)
    {
        _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _queueName = options.Value.BrokerUrl;
        if (!_queueName.EndsWith("/"))
            _queueName = $"{_queueName}/";

        _queueName = $"{_queueName}{options.Value.ExchangeName}";
    }

    public async Task SendAsync<T>(T command) where T : class, ICommand
    {
        try
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri(_queueName));
            await endpoint.Send(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task RegisterHandlerAsync<T>(Action<T> handler) where T : IMessage
    {
        return Task.CompletedTask;
    }

    public async Task PublishAsync(IMessage @event)
    {
        await _busControl.Publish(@event);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"MassTransit BusControl Started at {DateTime.Now}");
        return _busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"MassTransit BusControl Stopped at {DateTime.Now}");
        return _busControl.StopAsync(cancellationToken);
    }
}