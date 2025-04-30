using FluentValidation;
using SistemaFinanceiro.Communication.Requests;

namespace SistemaFinanceiro.Application.UseCases.Despesas
{
    public class RegistraDespesasValidator: AbstractValidator<RequestDespesaJson>
    {
        public RegistraDespesasValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("O titulo é obrigatórios");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valo precisa ser maior que 0");
            RuleFor(x => x.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data precisa ser anterior a atual");
            RuleFor(x => x.TipoPagto).IsInEnum().WithMessage("Tipo de pagamento Invalido");
        }
    }
}
