using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesasReadOnlyRepository
    {
        public Task<List<Despesa>> GetAll();
        public Task<Despesa?> GetById(long id);
        public Task<List<Despesa>> GetByMes(DateOnly mes);
    }
}
