using SistemaFinanceiro.Domain.Enums;

namespace SistemaFinanceiro.Domain.Entities
{
    public class Despesa
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoPagto TipoPagto { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];
        

        public long UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
