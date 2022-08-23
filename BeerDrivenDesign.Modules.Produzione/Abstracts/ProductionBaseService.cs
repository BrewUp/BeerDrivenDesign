using BeerDrivenDesign.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public abstract class ProductionBaseService
{
    protected readonly IPersister Persister;
    protected readonly ILogger Logger;

    protected ProductionBaseService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}