using FluentValidation;
using BaseDotnet.Application.DTOs;

namespace BaseDotnet.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("SKU must only contain uppercase alphanumeric characters and hyphens.");
        }
    }
}
