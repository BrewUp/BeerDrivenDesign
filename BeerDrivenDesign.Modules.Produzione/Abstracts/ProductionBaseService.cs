using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public abstract class ProductionBaseService
{
    protected readonly ILogger Logger;

    protected ProductionBaseService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}