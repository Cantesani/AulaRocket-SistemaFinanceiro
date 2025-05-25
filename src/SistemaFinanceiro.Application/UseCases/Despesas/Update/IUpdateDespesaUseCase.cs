using SistemaFinanceiro.Communication.Requests.Despesas;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Update
{
    public interface IUpdateDespesaUseCase
    {
        public Task Execute(long id, RequestDespesaJson request);
    }
}
