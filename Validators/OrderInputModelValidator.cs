using EComerceApi.Data;
using EComerceApi.InputModel;
using EComerceApi.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Validators;

public class OrderInputModelValidator : AbstractValidator<OrderInputModel>
{
    public OrderInputModelValidator(AppDbContext context)
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(Guid.Empty).WithMessage
                ("O ID do cliente é obrigatório e deve ser maior que zero.");
         async Task<bool> CustomerExists(int customerId, CancellationToken cancellationToken)
        {
            return await context.Customers.AnyAsync(c => Equals(c.Id, customerId), cancellationToken);
        }
        
        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemValidator(context));
    }
}