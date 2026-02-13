using CLTSalesSystem.Application.DTOs;
using FluentValidation;

namespace CLTSalesSystem.Application.Validators
{
    public class VentaValidator : AbstractValidator<CreateVentaDTO>
    {
        public VentaValidator()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("Debe seleccionar un cliente válido");

            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("La venta debe tener al menos un detalle");

            RuleForEach(x => x.Detalles).SetValidator(new DetalleVentaValidator());
        }
    }

    public class DetalleVentaValidator : AbstractValidator<CreateDetalleVentaDTO>
    {
        public DetalleVentaValidator()
        {
            RuleFor(x => x.ProductoId)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto válido");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
        }
    }
}
