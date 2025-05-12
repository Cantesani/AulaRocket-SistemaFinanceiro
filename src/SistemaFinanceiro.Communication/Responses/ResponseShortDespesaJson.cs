using SistemaFinanceiro.Communication.Enums;

namespace SistemaFinanceiro.Communication.Responses
{
    public class ResponseShortDespesaJson
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
