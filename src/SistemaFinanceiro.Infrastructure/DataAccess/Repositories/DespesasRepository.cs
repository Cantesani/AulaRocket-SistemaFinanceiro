using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Despesas;

namespace SistemaFinanceiro.Infrastructure.DataAccess.Repositories
{
    internal class DespesasRepository : IDespesasReadOnlyRepository, IDespesasWriteOnlyRepository
    {
        private readonly SistemaFinanceiroDbContext _dbContext;
        public DespesasRepository(SistemaFinanceiroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Despesa despesa)
        {
            await _dbContext.Despesas.AddAsync(despesa);
        }

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext.Despesas.FirstOrDefaultAsync(x=>x.Id == id);

            if(result is null)
                return false;

            _dbContext.Despesas.Remove(result);

            return true;
        }

        public async Task<List<Despesa>> GetAll()
        {
            return await _dbContext.Despesas.AsNoTracking().ToListAsync();
        }

        public async Task<Despesa?> GetById(long id)
        {
            return await _dbContext.Despesas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
