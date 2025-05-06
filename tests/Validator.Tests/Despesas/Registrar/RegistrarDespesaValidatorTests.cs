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
            var request = new RequestDespesaJson
            {
                Valor = 100,
                Data = DateTime.Now.AddDays(-1),
                Descricao = "Descrição",
                Titulo = "Samsung",
                TipoPagto = SistemaFinanceiro.Communication.Enums.TipoPagto.Debito
            };

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            Assert.True(result.IsValid);
        }
    }
}
