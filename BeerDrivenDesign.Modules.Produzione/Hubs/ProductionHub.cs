using BrewUp.Shared.Messages.CustomTypes;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Hubs;

public class ProductionHub : Hub
{
    private readonly IHubContext<ProductionHub> _context;
    private readonly ILogger _logger;

    public ProductionHub(IHubContext<ProductionHub> context,
        ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    internal async Task ProductionOrderStartedUpdatedAsync(BatchId batchId)
    {
        await _context.Clients.All.SendAsync("beerProductionStarted", batchId.ToString());
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Clients connected: {_context.Clients.ToString()}");
        await _context.Clients.All.SendAsync("beerProductionStarted", "Welcome");

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception != null)
            _logger.LogInformation($"Someone has lost connection: {exception.Message}");
        
        return base.OnDisconnectedAsync(exception);
    }
}