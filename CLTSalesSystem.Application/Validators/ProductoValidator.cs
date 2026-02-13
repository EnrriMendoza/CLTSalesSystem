using CLTSalesSystem.Application.DTOs;
using FluentValidation;

namespace CLTSalesSystem.Application.Validators
{
    public class ProductoValidator : AbstractValidator<CreateProductoDTO>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres");

            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}
