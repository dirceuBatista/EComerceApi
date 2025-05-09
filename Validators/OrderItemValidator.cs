using EComerceApi.Data;
using EComerceApi.Models;
using EComerceApi.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Validators;

public class OrderItemValidator : AbstractValidator<OrderItemViewModel>
{
    public OrderItemValidator(AppDbContext context)
    {
            
        RuleFor(x => x.ProductId)
            .GreaterThan(Guid.Empty).WithMessage("O ID do produto é obrigatório e deve ser maior que zero.");
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

         async Task<dynamic> BeCorrectPrice(OrderItem item, decimal unitPrice, CancellationToken ct)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(x => x.Id == item.ProductId, ct);

            if (product == null)
                return false;
            return unitPrice = product.Price;
        }
    }

    
}