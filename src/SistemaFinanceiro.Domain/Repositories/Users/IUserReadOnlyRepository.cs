using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Users
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExisteUserComEsseEmail(string email);

        public Task<User?> GetUserByEmail(string email);
    }
}
