using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Despesas
{
    public interface IDespesasWriteOnlyRepository
    {
        public Task Add(Despesa despesa);

        /// <summary>
        /// Essa funcao retorna TRUE se deletou com sucesso. Senão, retorna FALSE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Delete(long id);
    }
}
