using Muflone.Core;

namespace BrewUp.Shared.Messages.CustomTypes;

public class BeerId : DomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}