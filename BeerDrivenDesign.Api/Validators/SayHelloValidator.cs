using BeerDrivenDesign.Models;
using FluentValidation;

namespace BeerDrivenDesign.Validators;

public class SayHelloValidator : AbstractValidator<HelloRequest>
{
    public SayHelloValidator()
    {
        RuleFor(h => h.Name).NotEmpty().MaximumLength(50);
    }
}