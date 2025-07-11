using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses.Users;

namespace SistemaFinanceiro.Application.UseCases.Users.Registrar
{
    public interface IRegistraUserUseCase
    {
        public Task<ResponseRegistraUserJson> Execute(RequestRegistraUserJson request);
    }
}
