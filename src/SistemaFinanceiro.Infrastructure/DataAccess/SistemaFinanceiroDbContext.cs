using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Infrastructure.DataAccess
{
    internal class SistemaFinanceiroDbContext: DbContext
    {
        public SistemaFinanceiroDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Despesa> Despesas { get; set; }
    }
}
