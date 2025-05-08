using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesasRepository
    {
        public void Add(Despesa despesa);
    }
}
