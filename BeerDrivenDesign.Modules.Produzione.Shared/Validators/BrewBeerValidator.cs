using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using FluentValidation;

namespace BeerDrivenDesign.Modules.Produzione.Shared.Validators;

public class BrewBeerValidator : AbstractValidator<PostBrewBeer>
{
    public BrewBeerValidator()
    {
        RuleFor(h => h.Quantity).GreaterThan(0);
        RuleFor(h => h.BeerId).NotEmpty();
        RuleFor(h => h.BeerType).NotEmpty();
        RuleFor(h => h.BatchId).NotEmpty();
    }
}