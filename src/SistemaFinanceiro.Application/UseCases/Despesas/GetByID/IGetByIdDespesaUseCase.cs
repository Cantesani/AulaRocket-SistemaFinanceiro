using SistemaFinanceiro.Communication.Responses.Despesas;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetByID
{
    public interface IGetByIdDespesaUseCase
    {
        public Task<ResponseDespesaJson> execute(long Id);
    }
}
