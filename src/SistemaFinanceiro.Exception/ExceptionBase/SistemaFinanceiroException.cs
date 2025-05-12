namespace SistemaFinanceiro.Exception.ExceptionBase
{
    public abstract class SistemaFinanceiroException : SystemException
    {
        protected SistemaFinanceiroException(string message) : base(message)
        {
        }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
