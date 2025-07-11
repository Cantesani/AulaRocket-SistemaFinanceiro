using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Infrastructure.DataAccess
{
    public class SistemaFinanceiroDbContext: DbContext
    {
        public SistemaFinanceiroDbContext(DbContextOptions options) : base(options) { }


        //Tabelas Banco
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
