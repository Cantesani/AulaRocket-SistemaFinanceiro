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
    internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
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

        public async Task Delete(User user)
        {
            var usuarioDeletar = await _dbContext.Users.FindAsync(user.Id);
            _dbContext.Users.Remove(usuarioDeletar!);
        }

        public async Task<bool> ExisteUserComEsseEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Email.Equals(email));
        }

        public async Task<User> GetById(long id)
        {
            return await _dbContext.Users.FirstAsync(x => x.Id == id);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
