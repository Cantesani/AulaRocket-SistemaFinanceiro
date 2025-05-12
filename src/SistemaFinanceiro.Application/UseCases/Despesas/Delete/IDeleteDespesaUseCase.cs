namespace SistemaFinanceiro.Application.UseCases.Despesas.Delete
{
    public interface IDeleteDespesaUseCase
    {
        public Task Execute(long id);
    }
}
