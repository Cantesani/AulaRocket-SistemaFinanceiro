using System.Net;

namespace SistemaFinanceiro.Exception.ExceptionBase
{
    public class ErroValidacaoException : SistemaFinanceiroException
    {
        private readonly List<string> _errors;

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public ErroValidacaoException(List<string> errorMessages): base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrors()
        {
            return _errors;
        }
    }
}
