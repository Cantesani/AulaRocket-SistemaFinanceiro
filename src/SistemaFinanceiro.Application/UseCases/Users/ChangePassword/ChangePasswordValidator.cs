using FluentValidation;
using SistemaFinanceiro.Communication.Requests.Users;

namespace SistemaFinanceiro.Application.UseCases.Users.ChangePassword;

public class ChangePasswordValidator: AbstractValidator<RequestChangePasswordUserJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordUserJson>());
    }
}