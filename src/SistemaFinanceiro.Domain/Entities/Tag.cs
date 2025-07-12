namespace SistemaFinanceiro.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    
    public Enums.Tag Valor { get; set; }

    
    public long DespesaId { get; set; }
    
    public Despesa Despesa { get; set; } = default!;
}