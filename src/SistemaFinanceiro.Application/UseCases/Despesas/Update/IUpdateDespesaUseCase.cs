using SistemaFinanceiro.Communication.Requests;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Update
{
    public interface IUpdateDespesaUseCase
    {
        public Task Execute(long id, RequestDespesaJson request);
    }
}
