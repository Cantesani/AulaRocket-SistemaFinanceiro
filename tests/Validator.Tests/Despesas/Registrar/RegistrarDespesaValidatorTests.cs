using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;

namespace Validator.Tests.Despesas.Registrar
{
    public class RegistrarDespesaValidatorTests
    {
        [Fact]
        public void Sucesso()
        {
            //ARANGE
            var validator = new RegistrarDespesaValidator();
            var request = RequestDespesaJsonBuilder.Builder();

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            //result.IsValid.Should().BeTrue();
        }
    }
}
