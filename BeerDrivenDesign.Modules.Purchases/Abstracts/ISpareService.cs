namespace BeerDrivenDesign.Modules.Purchases.Abstracts;

public interface ISpareService
{
    Task<bool> VerifySpare(string codiceArticolo);
}