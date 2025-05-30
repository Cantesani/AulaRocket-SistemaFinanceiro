using FluentValidation;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Exception;

namespace SistemaFinanceiro.Application.UseCases.Users.Registrar
{
    public class RegistrarUserValidator : AbstractValidator<RequestRegistraUserJson>
    {
        public RegistrarUserValidator() {
            RuleFor(x => x.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_EMPTY)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);

            RuleFor(x => x.Password).SetValidator(new PasswordValidator<RequestRegistraUserJson>());
        }
    }
}
