using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesasReadOnlyRepository
    {
        public Task<List<Despesa>> GetAll();
        public Task<Despesa?> GetById(long id);
    }
}
