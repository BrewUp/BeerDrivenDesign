using Muflone.Core;

namespace BeerDrivenDesign.Modules.Produzione.CustomTypes;

public class BeerId : DomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}