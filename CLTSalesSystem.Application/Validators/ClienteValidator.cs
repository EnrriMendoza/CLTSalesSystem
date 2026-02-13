using CLTSalesSystem.Application.DTOs;
using FluentValidation;

namespace CLTSalesSystem.Application.Validators
{
    public class ClienteValidator : AbstractValidator<CreateClienteDTO>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio");
        }
    }
}
