using SistemaFinanceiro.Communication.Enums;

namespace SistemaFinanceiro.Communication.Requests.Despesas
{
    public class RequestDespesaJson
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoPagto TipoPagto { get; set; }
    }
}
