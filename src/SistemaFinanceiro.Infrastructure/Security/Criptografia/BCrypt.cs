using SistemaFinanceiro.Domain.Security.Criptografia;
using BC = BCrypt.Net.BCrypt;

namespace SistemaFinanceiro.Infrastructure.Security.Criptografia
{
    public class BCrypt : IPasswordCriptografada
    {
        public string Criptografar(string password)
        {
            string passwordHash = BC.HashPassword(password);
             
            return passwordHash;
        }
    }
}
