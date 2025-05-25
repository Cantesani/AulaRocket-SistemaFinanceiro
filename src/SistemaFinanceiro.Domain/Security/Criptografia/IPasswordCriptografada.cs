using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Domain.Security.Criptografia
{
    public interface IPasswordCriptografada
    {
        public string Criptografar(string password);
    }
}
