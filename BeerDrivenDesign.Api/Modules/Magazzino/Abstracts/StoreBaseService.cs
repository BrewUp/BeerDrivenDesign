using BeerDrivenDesign.ReadModel.Abstracts;

namespace BeerDrivenDesign.Api.Modules.Magazzino.Abstracts;

public abstract class StoreBaseService
{
    protected readonly IPersister Persister;
    protected readonly ILogger Logger;

    protected StoreBaseService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}