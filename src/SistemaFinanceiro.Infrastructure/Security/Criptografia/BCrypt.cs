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

        public bool VerificaSenha(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
