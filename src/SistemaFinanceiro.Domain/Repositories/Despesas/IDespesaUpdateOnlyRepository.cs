using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesaUpdateOnlyRepository
    {
        Task<Despesa?> GetById(long id);
        public void Update(Despesa despesa);
    }
}
