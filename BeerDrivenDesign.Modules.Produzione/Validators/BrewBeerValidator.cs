using BeerDrivenDesign.Modules.Produzione.DTO;
using FluentValidation;

namespace BeerDrivenDesign.Modules.Produzione.Validators;

public class BrewBeerValidator : AbstractValidator<BrewBeer>
{
    public BrewBeerValidator()
    {
        RuleFor(h => h.Quantity).GreaterThan(0);
        RuleFor(h => h.BeerType).NotEmpty();
    }
}