using BrewUp.Shared.Messages.CustomTypes;
using BrewUp.Shared.Messages.Events;
using Microsoft.AspNetCore.SignalR;

namespace BeerDrivenDesign.Modules.Produzione.Hubs;

public class ProductionHub : Hub
{
    private readonly IHubContext<ProductionHub> _context;
    public ProductionHub(IHubContext<ProductionHub> context)
    {
        _context = context;
    }

    internal async Task ProductionOrderUpdated(BatchId batchId)
    {
        await _context.Clients.All.SendAsync(nameof(BeerProductionStarted), batchId.ToString());
    }
}