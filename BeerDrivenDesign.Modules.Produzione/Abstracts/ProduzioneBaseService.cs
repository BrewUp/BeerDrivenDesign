using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Produzione.Abstracts;

public abstract class ProduzioneBaseService
{
    protected readonly ILogger Logger;

    protected ProduzioneBaseService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}