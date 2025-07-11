using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Users.Update;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Exception;

namespace Validator.Tests.Users.Update
{
    public class UpdateUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Builder();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Error_Nome_Empty(string nome)
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Builder();
            request.Nome = nome;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO));
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Builder();
            request.Email = email;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_USUARIO_EMPTY));
        }
        
        [Fact]
        public void Error_Email_Invalido()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Builder();
            request.Email = "teste.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO));
        }






    }
}
