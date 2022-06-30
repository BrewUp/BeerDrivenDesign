using BeerDrivenDesign.Api.Models;
using FluentValidation;

namespace BeerDrivenDesign.Api.Validators;

public class SayHelloValidator : AbstractValidator<HelloRequest>
{
    public SayHelloValidator()
    {
        RuleFor(h => h.Name).NotEmpty().MaximumLength(50);
    }
}