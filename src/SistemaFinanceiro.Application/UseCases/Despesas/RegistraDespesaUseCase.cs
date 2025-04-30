using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Application.UseCases.Despesas
{
    public class RegistraDespesaUseCase
    {
        public ResponseDespesaJson Execute(RequestDespesaJson request)
        {
            Validate(request);
            return new ResponseDespesaJson();
        }

        public void Validate(RequestDespesaJson request) {
            var validator = new RegistraDespesasValidator();
            var result = validator.Validate(request);

        }
    }
}
