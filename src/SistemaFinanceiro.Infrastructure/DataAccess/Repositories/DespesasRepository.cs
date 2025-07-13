using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Despesas;

namespace SistemaFinanceiro.Infrastructure.DataAccess.Repositories
{
    internal class DespesasRepository : IDespesasReadOnlyRepository, IDespesasWriteOnlyRepository, IDespesaUpdateOnlyRepository
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

        public async Task Delete(long id)
        {
            var result = await _dbContext.Despesas.FindAsync(id);
            _dbContext.Despesas.Remove(result!);
        }

        public async Task<List<Despesa>> GetAll(long userId)
        {
            return await _dbContext.Despesas.AsNoTracking().Where(x=>x.UserId == userId) .ToListAsync();
        }

        async Task<Despesa?> IDespesasReadOnlyRepository.GetById(long id, long userId)
        {
            return await _dbContext.Despesas
                .Include(x=>x.Tags)
                .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        async Task<Despesa?> IDespesaUpdateOnlyRepository.GetById(long id, long userId)
        {
            return await _dbContext .Despesas
                .Include(x=>x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public void Update(Despesa despesa)
        {
            _dbContext.Despesas.Update(despesa);
        }

        public async Task<List<Despesa>> GetByMes(User user, DateOnly mes)
        {
            var dataInicial = new DateTime(year: mes.Year, month: mes.Month, day: 1).Date;
            var ultimoDiaMesInformado = DateTime.DaysInMonth(year: mes.Year, month: mes.Month);
            var dataFinal = new DateTime(year: mes.Year, month: mes.Month, day: ultimoDiaMesInformado, hour: 23, minute: 59, second: 59);

            return await _dbContext
                .Despesas
                .AsNoTracking()
                .Where(x => x.UserId == user.Id && x.Data >= dataInicial && x.Data <= dataFinal)
                .OrderBy(x => x.Data)
                .ThenBy(x => x.Titulo)
                .ToListAsync();
        }
    }
}
