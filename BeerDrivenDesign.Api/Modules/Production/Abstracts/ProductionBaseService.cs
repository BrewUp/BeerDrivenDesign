using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.Api.Modules.Production.Abstracts;

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