using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Domain.Repositories.Users
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExisteUserComEsseEmail(string email);
    }
}
