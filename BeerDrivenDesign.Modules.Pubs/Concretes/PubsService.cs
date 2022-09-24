using BeerDrivenDesign.Modules.Pubs.Abstracts;
using BeerDrivenDesign.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BeerDrivenDesign.Modules.Pubs.Concretes;

public sealed class PubsService : PubsBaseService, IPubsService
{
    public PubsService(IPersister persister,
        ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }
}