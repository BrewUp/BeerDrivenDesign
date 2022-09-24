using BeerDrivenDesign.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Pubs.Abstracts;

public abstract class PubsBaseService
{
    protected readonly IPersister Persister;
    protected readonly ILogger Logger;

    protected PubsBaseService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}