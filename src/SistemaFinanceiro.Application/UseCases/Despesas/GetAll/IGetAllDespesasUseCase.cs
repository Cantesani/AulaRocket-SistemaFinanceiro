using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetAll
{
    public interface IGetAllDespesasUseCase
    {
        public Task<ResponseLstDespesasJson> Execute();
    }
}
