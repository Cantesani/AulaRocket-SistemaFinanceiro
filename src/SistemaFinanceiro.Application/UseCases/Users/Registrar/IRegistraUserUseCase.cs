using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Application.UseCases.Users.Registrar
{
    public interface IRegistraUserUseCase
    {
        public Task<ResponseRegistraUserJson> Execute(RequestRegistraUserJson request);
    }
}
