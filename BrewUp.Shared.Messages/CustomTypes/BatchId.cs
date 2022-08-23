using Muflone.Core;

namespace BrewUp.Shared.Messages.CustomTypes;

public sealed class BatchId : DomainId
{
    public BatchId(Guid value) : base(value)
    {
    }
}