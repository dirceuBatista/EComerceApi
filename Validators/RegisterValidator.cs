using EComerceApi.ViewModels;
using FluentValidation;

namespace EComerceApi.Validators;

public class RegisterValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome é requerido");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email é requerido")
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage("O slug é requerido");
    }

}

    