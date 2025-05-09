using EComerceApi.ViewModels;
using FluentValidation;

namespace EComerceApi.Validators;

public class UserValidator : AbstractValidator<UserViewModel>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(user => user.Slug)
            .NotEmpty().WithMessage("O slug é obrigatório.");


    }
}