using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Users
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add (User user);
    }
}
