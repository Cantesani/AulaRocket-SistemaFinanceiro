using SistemaFinanceiro.Communication.Enums;

namespace SistemaFinanceiro.Communication.Responses.Despesas
{
    public class ResponseDespesaJson
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoPagto TipoPagto { get; set; }
        
        public IList<Tag> Tags { get; set; } = [];
    }
}
