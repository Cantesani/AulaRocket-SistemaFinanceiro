using SistemaFinanceiro.Communication.Requests.Users;

namespace SistemaFinanceiro.Application.UseCases.Users.ChangePassword;

public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordUserJson  request);
}