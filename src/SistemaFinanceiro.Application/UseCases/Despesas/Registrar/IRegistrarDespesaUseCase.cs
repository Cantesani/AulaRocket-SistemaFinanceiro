using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Registrar
{
    public interface IRegistrarDespesaUseCase
    {
        ResponseDespesaJson Execute(RequestDespesaJson request);
    }
}
