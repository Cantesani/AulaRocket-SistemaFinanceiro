using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        string Generate(User user);
    }
}
