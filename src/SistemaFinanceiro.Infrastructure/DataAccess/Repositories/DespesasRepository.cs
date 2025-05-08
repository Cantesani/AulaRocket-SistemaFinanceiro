using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Despesas;

namespace SistemaFinanceiro.Infrastructure.DataAccess.Repositories
{
    internal class DespesasRepository : IDespesasRepository
    {
        private readonly SistemaFinanceiroDbContext _dbContext;
        public DespesasRepository(SistemaFinanceiroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Despesa despesa)
        {
            _dbContext.Add(despesa);
        } 
    }
}
