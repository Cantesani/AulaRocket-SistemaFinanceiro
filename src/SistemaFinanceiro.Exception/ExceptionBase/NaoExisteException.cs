using System.Net;

namespace SistemaFinanceiro.Exception.ExceptionBase
{
    public class NaoExisteException: SistemaFinanceiroException
    {
        public NaoExisteException(string message) : base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
