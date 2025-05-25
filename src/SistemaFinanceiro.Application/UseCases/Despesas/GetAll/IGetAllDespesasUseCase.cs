using SistemaFinanceiro.Communication.Responses.Despesas;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetAll
{
    public interface IGetAllDespesasUseCase
    {
        public Task<ResponseLstDespesasJson> Execute();
    }
}
