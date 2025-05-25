using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Infrastructure.DataAccess;
using static System.Formats.Asn1.AsnWriter;

namespace SistemaFinanceiro.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public static async Task MigrateDataBase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<SistemaFinanceiroDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
