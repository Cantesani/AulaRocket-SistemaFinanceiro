using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Despesas;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Communication.Enums;
using SistemaFinanceiro.Exception;

namespace Validator.Tests.Despesas.Registrar
{
    public class RegistrarDespesaValidatorTests
    {
        [Fact]
        public void Sucesso()
        {
            //ARANGE
            var validator = new DespesaValidator();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Error_Titulo_Branco(string titulo)
        {
            //ARANGE
            var validator = new DespesaValidator();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.Titulo = titulo;

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.TITULO_OBRIGATORIO));
        }

        [Fact]
        public void Error_Data_Futura()
        {
            //ARANGE
            var validator = new DespesaValidator();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.Data = DateTime.Now.AddDays(1);

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.DATA_PRECISA_SER_ANTERIOR_A_ATUAL));
        }

        [Fact]
        public void Error_Tipo_Pagamento_Invalido()
        {
            //ARANGE
            var validator = new DespesaValidator();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.TipoPagto = (TipoPagto)50;

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.TIPO_PAGAMENTO_INVALIDO));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_Valor_Invalido(decimal valor)
        {
            //ARANGE
            var validator = new DespesaValidator();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.Valor = valor;

            //ACT
            var result = validator.Validate(request);

            //ASSERT
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.VALOR_PRECISA_SER_MAIOR_QUE_ZERO));
        }
    }
}
