using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Registrar
{
    public class RegistrarDespesaUseCase
    {
        public ResponseDespesaJson Execute(RequestDespesaJson request)
        {
            Validate(request);
            return new ResponseDespesaJson();
        }

        public void Validate(RequestDespesaJson request)
        {
            var validator = new RegistrarDespesaValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErroValidacaoException(errorMessage);
            }

        }
    }
}
