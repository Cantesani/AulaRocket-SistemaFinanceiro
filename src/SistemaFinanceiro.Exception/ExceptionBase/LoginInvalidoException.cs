using System.Net;

namespace SistemaFinanceiro.Exception.ExceptionBase
{
    public class LoginInvalidoException: SistemaFinanceiroException
    {
        public LoginInvalidoException(): base(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
