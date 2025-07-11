using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesasReadOnlyRepository
    {
        public Task<List<Despesa>> GetAll(long userId);
        public Task<Despesa?> GetById(long id, long userId);
        public Task<List<Despesa>> GetByMes(Entities.User user, DateOnly mes);
    }
}
