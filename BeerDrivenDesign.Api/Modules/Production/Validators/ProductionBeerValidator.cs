using BeerDrivenDesign.Modules.Produzione.Shared.Dtos;
using BeerDrivenDesign.ReadModel.Dtos;
using FluentValidation;

namespace BeerDrivenDesign.Modules.Produzione.Shared.Validators;

public class ProductionBeerValidator : AbstractValidator<PostProductionBeer>
{
    public ProductionBeerValidator()
    {
        RuleFor(h => h.Quantity).GreaterThan(0);
        RuleFor(h => h.BeerId).NotEmpty();
        RuleFor(h => h.BeerType).NotEmpty();
        RuleFor(h => h.BatchNumber).NotEmpty();
        RuleFor(h => h.ProductionTime).GreaterThan(DateTime.MinValue);
    }
}