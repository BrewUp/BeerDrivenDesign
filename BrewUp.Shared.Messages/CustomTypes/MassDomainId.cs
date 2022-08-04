using Muflone.Core;

namespace BrewUp.Shared.Messages.CustomTypes;

public abstract class MassDomainId : DomainId
{
    protected MassDomainId() : base(Guid.Empty)
    {}

    protected MassDomainId(Guid value) : base(value)
    {}
}