using E_Tracker.Application.ViewModels.Producrs;
using FluentValidation;

namespace E_Tracker.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter Product Name")
                .MaximumLength(100)
                .MinimumLength(5);
            RuleFor(s => s.Stock)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Stock can not be empty")
                .Must(s => s > 0);
            RuleFor(s => s.Price)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Price can not be 0")
                .Must(s => s >= 0);

        }
    }
}
