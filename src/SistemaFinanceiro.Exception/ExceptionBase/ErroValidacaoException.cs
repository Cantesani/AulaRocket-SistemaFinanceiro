namespace SistemaFinanceiro.Exception.ExceptionBase
{
    public class ErroValidacaoException : SistemaFinanceiroException
    {
        public List<string> Errors { get; set; }
        public ErroValidacaoException(List<string> errorMessages)
        {
            Errors = errorMessages;
        }
    }
}
