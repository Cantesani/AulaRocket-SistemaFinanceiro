using SistemaFinanceiro.Communication.Requests.Users;

namespace SistemaFinanceiro.Application.UseCases.Users.Update
{
    public interface IUpdateUserUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
