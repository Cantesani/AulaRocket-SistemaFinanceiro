using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetByID
{
    public interface IGetByIdDespesaUseCase
    {
        public Task<ResponseDespesaJson> execute(long Id);
    }
}
