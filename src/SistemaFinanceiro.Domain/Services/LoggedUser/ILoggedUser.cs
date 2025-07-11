using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Services.LoggerUser
{
    public interface ILoggedUser
    {
        public Task<User> Get();
    }
}
