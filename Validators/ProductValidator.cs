using EComerceApi.ViewModels;
using FluentValidation;

namespace EComerceApi.Validators;

public class ProductValidator : AbstractValidator<ProductViewModel>
{
    public ProductValidator()
    {
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O Nome do produto é requerido");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("O preço do produto deve ser maior que zero");

        RuleFor(x => x.InStock)
            .NotNull()
            .WithMessage("O campo InStock deve ser informado");
    }
}