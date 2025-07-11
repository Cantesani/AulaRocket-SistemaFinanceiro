using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Repositories.Users
{
    public interface IUserUpdateOnlyRepository
    {
        Task<User> GetById(long id);
        void Update(User user); 
    }
}
