using SistemaFinanceiro.Communication.Requests.Despesas;
using SistemaFinanceiro.Communication.Responses.Despesas;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Registrar
{
    public interface IRegistrarDespesaUseCase
    {
        Task<ResponseDespesaJson> Execute(RequestDespesaJson request);
    }
}
