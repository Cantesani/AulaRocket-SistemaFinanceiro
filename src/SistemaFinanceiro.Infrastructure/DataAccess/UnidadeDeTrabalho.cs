using SistemaFinanceiro.Domain.Repositories;

namespace SistemaFinanceiro.Infrastructure.DataAccess
{
    internal class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private readonly SistemaFinanceiroDbContext _dbContext;
        public UnidadeDeTrabalho(SistemaFinanceiroDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
