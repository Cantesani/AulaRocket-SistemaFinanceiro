using FluentValidation;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Exception;

namespace SistemaFinanceiro.Application.UseCases.Despesas
{
    public class DespesaValidator : AbstractValidator<RequestDespesaJson>
    {
        public DespesaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage(ResourceErrorMessages.TITULO_OBRIGATORIO);
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage(ResourceErrorMessages.VALOR_PRECISA_SER_MAIOR_QUE_ZERO);
            RuleFor(x => x.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATA_PRECISA_SER_ANTERIOR_A_ATUAL);
            RuleFor(x => x.TipoPagto).IsInEnum().WithMessage(ResourceErrorMessages.TIPO_PAGAMENTO_INVALIDO);
        }
    }
}
