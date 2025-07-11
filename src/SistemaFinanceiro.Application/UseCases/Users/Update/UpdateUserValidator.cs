using FluentValidation;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;

namespace SistemaFinanceiro.Application.UseCases.Users.Update
{
    public class UpdateUserValidator: AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator() {
            RuleFor(x => x.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_EMPTY)
                .EmailAddress()
                .When(x => string.IsNullOrWhiteSpace(x.Email) == false, ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);
        }
    }
}
