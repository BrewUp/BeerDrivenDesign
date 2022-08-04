using Muflone.Core;

namespace BrewUp.Shared.Messages.CustomTypes;

public class BeerId : MassDomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}