using Muflone.Messages;
using Muflone.Transport.Azure.Factories;
using Muflone.Transport.Azure.Models;

namespace BeerDrivenDesign.Api.Modules.Production.Factories;

public sealed class AzureQueueReferenceFactory : IAzureQueueReferenceFactory
{
    public AzureQueueReferences Create<T>() where T : IMessage
    {
        return new AzureQueueReferences(typeof(T).Name, "",
            "");
    }
}