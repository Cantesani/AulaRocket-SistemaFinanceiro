using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository: IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly SistemaFinanceiroDbContext _dbContext;

        public UserRepository(SistemaFinanceiroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExisteUserComEsseEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Email.Equals(email));
        }
    }
}
 