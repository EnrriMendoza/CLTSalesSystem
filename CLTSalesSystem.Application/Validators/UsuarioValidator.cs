using CLTSalesSystem.Application.DTOs;
using FluentValidation;

namespace CLTSalesSystem.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria");
        }
    }

    public class RegistroUsuarioValidator : AbstractValidator<RegistroUsuarioDTO>
    {
        public RegistroUsuarioValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
        }
    }
}
