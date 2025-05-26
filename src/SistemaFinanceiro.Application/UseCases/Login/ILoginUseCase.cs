using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Communication.Responses.Users;

namespace SistemaFinanceiro.Application.UseCases.Login
{
    public interface ILoginUseCase
    {
        public Task<ResponseRegistraUserJson> Execute(RequestLoginJson login);
    }
}
